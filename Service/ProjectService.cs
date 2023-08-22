using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service;

internal sealed class ProjectService : IProjectService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDataShaper<ProjectDto> _dataShaper;

    public ProjectService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<ProjectDto> dataShaper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _dataShaper = dataShaper;
    }

    public async Task<(IEnumerable<ExpandoObject> projects, MetaData metaData)> GetAllProjectsAsync(ProjectParameters projectParameters, bool trackChanges)
    {
        if (!projectParameters.ValidPriorityRange)
            throw new PriorityRangeBadRequestException();

        var projectsWithMetaData = await _repository.Project.GetAllProjectsAsync(projectParameters, trackChanges);
        var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projectsWithMetaData);
        var shapedData = _dataShaper.ShapeData(projectsDto, projectParameters.Fields);

        return (projects: shapedData, metaData: projectsWithMetaData.MetaData);
    }

    public async Task<ProjectDto> GetProjectAsync(Guid id, bool trackChanges)
    {
        var projectDb = await GetProjectAndCheckIfItExists(id, trackChanges);
        var projectDto = _mapper.Map<ProjectDto>(projectDb);
        return projectDto;
    }

    public async Task<ProjectDto> GetProjectByEmployeeAsync(Guid employeeId, Guid projectId, bool trackChanges)
    {
        await CheckIfEmployeeExists(employeeId, trackChanges);
        var projectDb = await GetProjectByEmployeeAndCheckIfItExists(employeeId, projectId, trackChanges);
        var projectDto = _mapper.Map<ProjectDto>(projectDb);
        return projectDto;
    }

    public async Task<IEnumerable<ProjectDto>> GetProjectsByEmployeeAsync(Guid employeeId, bool trackChanges)
    {
        await CheckIfEmployeeExists(employeeId, trackChanges);
        var projectsFromDb = await _repository.Project.GetProjectsByEmployeeAsync(employeeId, trackChanges);
        var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projectsFromDb);
        return projectsDto;
    }

    public async Task<ProjectDto> CreateProjectAsync(ProjectForCreationDto project)
    {
        var projectEntity = _mapper.Map<Project>(project);
        _repository.Project.CreateProject(projectEntity);

        if (project.Employees is not null)
        {
            var employeeEntities = _mapper.Map<IEnumerable<Employee>>(project.Employees);

            foreach (var employeeEntity in employeeEntities)
            {
                _repository.Employee.CreateEmployee(employeeEntity);
                _repository.ProjectEmployee.CreateProjectEmployee(projectEntity.Id, employeeEntity.Id, null);
            }
        }

        await _repository.SaveAsync();

        var projectToReturn = _mapper.Map<ProjectDto>(projectEntity);
        return projectToReturn;
    }

    public async Task<IEnumerable<ProjectDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var projectEntities = await _repository.Project.GetByIdsAsync(ids, trackChanges);

        if (ids.Count() != projectEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var projectsToReturn = _mapper.Map<IEnumerable<ProjectDto>>(projectEntities);
        return projectsToReturn;
    }

    public async Task<(IEnumerable<ProjectDto> projects, string ids)> CreateProjectCollectionAsync(IEnumerable<ProjectForCreationDto> projectCollection)
    {
        if (projectCollection is null)
            throw new ProjectCollectionBadRequest();

        var projectEntities = _mapper.Map<IEnumerable<Project>>(projectCollection);

        foreach (var project in projectEntities)
        {
            _repository.Project.CreateProject(project);
        }

        await _repository.SaveAsync();

        var projectCollectionToReturn = _mapper.Map<IEnumerable<ProjectDto>>(projectEntities);
        var ids = string.Join(",", projectCollectionToReturn.Select(p => p.Id));
        return (projects: projectCollectionToReturn, ids);
    }

    public async Task DeleteProjectAsync(Guid id, bool trackChanges)
    {
        var projectDb = await GetProjectAndCheckIfItExists(id, trackChanges);
        _repository.Project.DeleteProject(projectDb);
        await _repository.SaveAsync();
    }

    public async Task DeleteEmployeeFromProjectAsync(Guid employeeId, Guid projectId, bool trackChanges)
    {
        await CheckIfEmployeeExists(employeeId, trackChanges);
        _ = await GetProjectByEmployeeAndCheckIfItExists(employeeId, projectId, trackChanges);

        var projectManagerDb = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectId, trackChanges);
        var projectEmployeeDb = await _repository.ProjectEmployee.GetProjectEmployeeAsync(projectId, employeeId, trackChanges);

        if (projectManagerDb is not null && projectManagerDb.Id.Equals(employeeId))
        {
            var projectEmployees = await _repository.ProjectEmployee.GetProjectEmployeesByProjectIdAsync(projectId, true);

            foreach (var pe in projectEmployees)
            {
                pe.ProjectManagerId = null;
            }
        }

        _repository.ProjectEmployee.DeleteProjectEmployee(projectEmployeeDb);
        await _repository.SaveAsync();
    }

    public async Task UpdateProjectAsync(Guid projectId, ProjectForUpdateDto projectForUpdate, bool trackChanges)
    {
        var projectDb = await GetProjectAndCheckIfItExists(projectId, trackChanges);
        _mapper.Map(projectForUpdate, projectDb);
        await _repository.SaveAsync();
    }

    public async Task<(ProjectForUpdateDto projectToPatch, Project projectEntity)> GetProjectForPatchAsync(Guid projectId, bool trackChanges)
    {
        var projectDb = await GetProjectAndCheckIfItExists(projectId, trackChanges);
        var projectToPatch = _mapper.Map<ProjectForUpdateDto>(projectDb);
        return (projectToPatch, projectDb);
    }

    public async Task SaveChangesForPatchAsync(ProjectForUpdateDto projectToPatch, Project projectEntity)
    {
        _mapper.Map(projectToPatch, projectEntity);
        await _repository.SaveAsync();
    }

    private async Task<Project> GetProjectAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var projectDb = await _repository.Project.GetProjectAsync(id, trackChanges);
        if (projectDb is null)
            throw new ProjectNotFoundException(id);
        return projectDb;
    }

    private async Task CheckIfEmployeeExists(Guid id, bool trackChanges)
    {
        var employeeDb = await _repository.Employee.GetEmployeeAsync(id, trackChanges);
        if (employeeDb is null)
            throw new EmployeeNotFoundException(id);
    }

    private async Task<Project> GetProjectByEmployeeAndCheckIfItExists(Guid employeeId, Guid projectId, bool trackChanges)
    {
        var projectDb = await _repository.Project.GetProjectByEmployeeAsync(employeeId, projectId, trackChanges);
        if (projectDb is null)
            throw new ProjectNotFoundException(projectId);
        return projectDb;
    }
}
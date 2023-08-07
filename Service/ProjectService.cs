using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class ProjectService : IProjectService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ProjectService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync(bool trackChanges)
    {
        var projects = await _repository.Project.GetAllProjectsAsync(trackChanges);
        var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projects);
        return projectsDto;
    }

    public async Task<ProjectDto> GetProjectAsync(Guid id, bool trackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(id, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(id);

        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }

    public async Task<ProjectDto> GetProjectByEmployeeAsync(Guid employeeId, Guid projectId, bool trackChanges)
    {
        var employee = await _repository.Employee.GetEmployeeAsync(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var project = await _repository.Project.GetProjectByEmployeeAsync(employeeId, projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }

    public async Task<IEnumerable<ProjectDto>> GetProjectsByEmployeeAsync(Guid employeeId, bool trackChanges)
    {
        var employee = await _repository.Employee.GetEmployeeAsync(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var projects = await _repository.Project.GetProjectsByEmployeeAsync(employeeId, trackChanges);
        var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projects);
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
        var project = await _repository.Project.GetProjectAsync(id, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(id);

        _repository.Project.DeleteProject(project);
        await _repository.SaveAsync();
    }

    public async Task DeleteEmployeeFromProjectAsync(Guid employeeId, Guid projectId, bool trackChanges)
    {
        var employee = await _repository.Employee.GetEmployeeAsync(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var projectForEmployee = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (projectForEmployee is null)
            throw new ProjectNotFoundException(projectId);

        var projectManager = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectId, trackChanges);
        var projectEmployee = await _repository.ProjectEmployee.GetProjectEmployeeAsync(projectId, employeeId, trackChanges);

        if (projectManager is not null && projectManager.Id.Equals(employeeId))
        {
            var projectEmployees = await _repository.ProjectEmployee.GetProjectEmployeesByProjectIdAsync(projectId, true);

            foreach (var pe in projectEmployees)
            {
                pe.ProjectManagerId = null;
            }
        }

        _repository.ProjectEmployee.DeleteProjectEmployee(projectEmployee);
        await _repository.SaveAsync();
    }

    public async Task UpdateProjectAsync(Guid projectId, ProjectForUpdateDto projectForUpdate, bool trackChanges)
    {
        var projectEntity = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (projectEntity is null)
            throw new ProjectNotFoundException(projectId);

        _mapper.Map(projectForUpdate, projectEntity);
        await _repository.SaveAsync();
    }

    public async Task<(ProjectForUpdateDto projectToPatch, Project projectEntity)> GetProjectForPatchAsync(Guid projectId, bool trackChanges)
    {
        var projectEntity = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (projectEntity is null)
            throw new ProjectNotFoundException(projectId);

        var projectToPatch = _mapper.Map<ProjectForUpdateDto>(projectEntity);
        return (projectToPatch, projectEntity);
    }

    public async Task SaveChangesForPatchAsync(ProjectForUpdateDto projectToPatch, Project projectEntity)
    {
        _mapper.Map(projectToPatch, projectEntity);
        await _repository.SaveAsync();
    }
}
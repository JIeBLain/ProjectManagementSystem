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

    public IEnumerable<ProjectDto> GetAllProjects(bool trackChanges)
    {
        var projects = _repository.Project.GetAllProjects(trackChanges);
        var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projects);
        return projectsDto;
    }

    public ProjectDto GetProject(Guid id, bool trackChanges)
    {
        var project = _repository.Project.GetProject(id, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(id);

        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }

    public ProjectDto GetProjectByEmployee(Guid employeeId, Guid projectId, bool trackChanges)
    {
        var employee = _repository.Employee.GetEmployee(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var project = _repository.Project.GetProjectByEmployee(employeeId, projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }

    public IEnumerable<ProjectDto> GetProjectsByEmployee(Guid employeeId, bool trackChanges)
    {
        var employee = _repository.Employee.GetEmployee(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var projects = _repository.Project.GetProjectsByEmployee(employeeId, trackChanges);
        var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projects);
        return projectsDto;
    }

    public ProjectDto CreateProject(ProjectForCreationDto project)
    {
        var projectEntity = _mapper.Map<Project>(project);

        _repository.Project.CreateProject(projectEntity);
        _repository.Save();

        var projectToReturn = _mapper.Map<ProjectDto>(projectEntity);
        return projectToReturn;
    }
}
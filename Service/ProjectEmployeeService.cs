using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class ProjectEmployeeService : IProjectEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ProjectEmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public IEnumerable<ProjectEmployeeDto> GetAllProjectEmployees(bool trackChanges)
    {
        var projectEmployees = _repository.ProjectEmployee.GetAllProjectEmployees(trackChanges);
        var projectEmployeesDto = _mapper.Map<IEnumerable<ProjectEmployeeDto>>(projectEmployees);
        return projectEmployeesDto;
    }

    public ProjectEmployeeDto GetProjectEmployee(Guid projectId, Guid employeeId, bool trackChanges)
    {
        var project = _repository.Project.GetProject(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employee = _repository.Employee.GetEmployee(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var projectEmployee = _repository.ProjectEmployee.GetProjectEmployee(projectId, employeeId, trackChanges);

        if (projectEmployee is null)
            throw new ProjectEmployeeNotFoundException(projectId, employeeId);

        var projectEmployeeDto = _mapper.Map<ProjectEmployeeDto>(projectEmployee);
        return projectEmployeeDto;
    }

    public ProjectEmployeeDto CreateProjectEmployee(ProjectEmployeeForCreationDto projectEmployee, bool trackChanges)
    {
        var projectEmployeeEntity = _mapper.Map<ProjectEmployee>(projectEmployee);

        var project = _repository.Project.GetProject(projectEmployeeEntity.ProjectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectEmployeeEntity.ProjectId);

        var employee = _repository.Employee.GetEmployee(projectEmployeeEntity.EmployeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(projectEmployeeEntity.EmployeeId);

        var projectManager = _repository.ProjectEmployee.GetProjectManagerByProject(projectEmployeeEntity.ProjectId, trackChanges);

        _repository.ProjectEmployee.CreateProjectEmployee(projectEmployeeEntity.ProjectId, projectEmployeeEntity.EmployeeId, projectEmployeeEntity.ProjectManagerId);
        _repository.Save();

        var projectEmployeeToReturn = new ProjectEmployeeDto(
            _mapper.Map<ProjectDto>(project),
            _mapper.Map<EmployeeDto>(employee),
            _mapper.Map<EmployeeDto>(projectManager));

        return projectEmployeeToReturn;
    }
}
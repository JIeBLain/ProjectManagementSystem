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

    public async Task<IEnumerable<ProjectEmployeeDto>> GetAllProjectEmployeesAsync(bool trackChanges)
    {
        var projectEmployees = await _repository.ProjectEmployee.GetAllProjectEmployeesAsync(trackChanges);
        var projectEmployeesDto = _mapper.Map<IEnumerable<ProjectEmployeeDto>>(projectEmployees);
        return projectEmployeesDto;
    }

    public async Task<ProjectEmployeeDto> GetProjectEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges)
    {
        var projectEmployee = await _repository.ProjectEmployee.GetProjectEmployeeAsync(projectId, employeeId, trackChanges);

        if (projectEmployee is null)
            throw new ProjectEmployeeNotFoundException(projectId, employeeId);

        var projectEmployeeDto = _mapper.Map<ProjectEmployeeDto>(projectEmployee);
        return projectEmployeeDto;
    }

    public async Task<ProjectEmployeeDto> CreateProjectEmployeeAsync(ProjectEmployeeForCreationDto projectEmployee, bool trackChanges)
    {
        var projectEmployeeEntity = _mapper.Map<ProjectEmployee>(projectEmployee);

        var project = await _repository.Project.GetProjectAsync(projectEmployeeEntity.ProjectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectEmployeeEntity.ProjectId);

        var employee = await _repository.Employee.GetEmployeeAsync(projectEmployeeEntity.EmployeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(projectEmployeeEntity.EmployeeId);

        var projectManager = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectEmployeeEntity.ProjectId, trackChanges);

        _repository.ProjectEmployee.CreateProjectEmployee(projectEmployeeEntity.ProjectId, projectEmployeeEntity.EmployeeId, projectEmployeeEntity.ProjectManagerId);
        await _repository.SaveAsync();

        var projectEmployeeToReturn = new ProjectEmployeeDto(
            _mapper.Map<ProjectDto>(project),
            _mapper.Map<EmployeeDto>(employee),
            _mapper.Map<EmployeeDto>(projectManager));

        return projectEmployeeToReturn;
    }
}
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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

    public async Task<(IEnumerable<ProjectEmployeeDto> projectEmployees, MetaData metaData)> GetAllProjectEmployeesAsync
        (ProjectEmployeeParameters projectEmployeeParameters, bool trackChanges)
    {
        var projectEmployeesWithMetaData = await _repository.ProjectEmployee.GetAllProjectEmployeesAsync(projectEmployeeParameters, trackChanges);
        var projectEmployeesDto = _mapper.Map<IEnumerable<ProjectEmployeeDto>>(projectEmployeesWithMetaData);
        return (projectEmployees: projectEmployeesDto, metaData: projectEmployeesWithMetaData.MetaData);
    }

    public async Task<ProjectEmployeeDto> GetProjectEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges)
    {
        var projectEmployeeDb = await _repository.ProjectEmployee.GetProjectEmployeeAsync(projectId, employeeId, trackChanges);

        if (projectEmployeeDb is null)
            throw new ProjectEmployeeNotFoundException(projectId, employeeId);

        var projectEmployeeDto = _mapper.Map<ProjectEmployeeDto>(projectEmployeeDb);
        return projectEmployeeDto;
    }

    public async Task<ProjectEmployeeDto> CreateProjectEmployeeAsync(ProjectEmployeeForCreationDto projectEmployee, bool trackChanges)
    {
        var projectEmployeeEntity = _mapper.Map<ProjectEmployee>(projectEmployee);

        var projectDb = await _repository.Project.GetProjectAsync(projectEmployeeEntity.ProjectId, trackChanges);

        if (projectDb is null)
            throw new ProjectNotFoundException(projectEmployeeEntity.ProjectId);

        var employeeDb = await _repository.Employee.GetEmployeeAsync(projectEmployeeEntity.EmployeeId, trackChanges);

        if (employeeDb is null)
            throw new EmployeeNotFoundException(projectEmployeeEntity.EmployeeId);

        var projectManagerDb = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectEmployeeEntity.ProjectId, trackChanges);

        _repository.ProjectEmployee.CreateProjectEmployee(projectEmployeeEntity.ProjectId, projectEmployeeEntity.EmployeeId, projectEmployeeEntity.ProjectManagerId);
        await _repository.SaveAsync();

        var projectEmployeeToReturn = new ProjectEmployeeDto
        {
            Project = _mapper.Map<ProjectDto>(projectDb),
            Employee = _mapper.Map<EmployeeDto>(employeeDb),
            ProjectManager = _mapper.Map<EmployeeDto>(projectManagerDb)
        };

        return projectEmployeeToReturn;
    }
}
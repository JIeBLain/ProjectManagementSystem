using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(bool trackChanges)
    {
        var employees = await _repository.Employee.GetAllEmployeesAsync(trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeesDto;
    }

    public async Task<EmployeeDto> GetEmployeeAsync(Guid id, bool trackChanges)
    {
        var employee = await _repository.Employee.GetEmployeeAsync(id, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(id);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }

    public async Task<EmployeeDto> GetEmployeeByProjectAsync(Guid projectId, Guid employeeId, bool trackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employee = await _repository.Employee.GetEmployeeAsync(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesByProjectAsync(Guid projectId, bool trackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employees = await _repository.Employee.GetEmployeesByProjectAsync(projectId, trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeesDto;
    }

    public async Task<EmployeeDto> GetProjectManagerByProjectAsync(Guid projectId, bool trackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var projectManager = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectId, trackChanges);

        if (projectManager is null)
            throw new ProjectManagerNotFoundException();

        var projectManagerDto = _mapper.Map<EmployeeDto>(projectManager);
        return projectManagerDto;
    }

    public async Task<EmployeeDto> GetProjectManagerByEmployeeAsync(Guid employeeId, bool trackChanges)
    {
        var employee = await _repository.Employee.GetEmployeeAsync(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var projectManager = await _repository.ProjectEmployee.GetProjectManagerByEmployeeAsync(employeeId, trackChanges);

        if (projectManager is null)
            throw new ProjectManagerNotFoundException();

        var projectManagerDto = _mapper.Map<EmployeeDto>(projectManager);
        return projectManagerDto;
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeForCreationDto employee)
    {
        var employeeEntity = _mapper.Map<Employee>(employee);

        _repository.Employee.CreateEmployee(employeeEntity);
        await _repository.SaveAsync();

        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;
    }

    public async Task<EmployeeDto> CreateEmployeeForProjectAsync(Guid projectId, EmployeeForCreationDto employeeFoCreation, bool trackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employeeEntity = _mapper.Map<Employee>(employeeFoCreation);
        _repository.Employee.CreateEmployee(employeeEntity);

        var projectManager = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectId, trackChanges);
        _repository.ProjectEmployee.CreateProjectEmployee(project.Id, employeeEntity.Id, projectManager.Id);

        await _repository.SaveAsync();

        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;
    }

    public async Task<EmployeeDto> CreateProjectManagerForProjectAsync(Guid projectId, EmployeeForCreationDto projectManagerForCreation, bool trackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var projectManagerEntity = _mapper.Map<Employee>(projectManagerForCreation);

        _repository.Employee.CreateEmployee(projectManagerEntity);
        _repository.ProjectEmployee.CreateProjectEmployee(project.Id, projectManagerEntity.Id, projectManagerEntity.Id);

        var projectEmployees = await _repository.ProjectEmployee.GetProjectEmployeesByProjectIdAsync(projectId, true);

        if (projectEmployees is not null)
        {
            foreach (var projectEmployee in projectEmployees)
            {
                projectEmployee.ProjectManager = projectManagerEntity;
            }
        }

        await _repository.SaveAsync();

        var projectManagerToReturn = _mapper.Map<EmployeeDto>(projectManagerEntity);
        return projectManagerToReturn;
    }

    public async Task<IEnumerable<EmployeeDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var employeeEntities = await _repository.Employee.GetByIdsAsync(ids, trackChanges);

        if (ids.Count() != employeeEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
        return employeesToReturn;
    }

    public async Task<(IEnumerable<EmployeeDto> employees, string ids)> CreateEmployeeCollectionAsync(IEnumerable<EmployeeForCreationDto> employeeCollection)
    {
        if (employeeCollection is null)
            throw new EmployeeCollectionBadRequest();

        var employeeEntities = _mapper.Map<IEnumerable<Employee>>(employeeCollection);

        foreach (var employee in employeeEntities)
        {
            _repository.Employee.CreateEmployee(employee);
        }

        await _repository.SaveAsync();

        var employeeCollectionToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
        var ids = string.Join(",", employeeCollectionToReturn.Select(e => e.Id));
        return (employees: employeeCollectionToReturn, ids);
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesWithoutProjectAsync(bool trackChanges)
    {
        var employees = await _repository.Employee.GetEmployeesWithoutProjectAsync(trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeesDto;
    }

    public async Task DeleteEmployeeAsync(Guid id, bool trackChanges)
    {
        var employee = await _repository.Employee.GetEmployeeAsync(id, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(id);

        var projectManager = await _repository.ProjectEmployee.GetProjectManagerByEmployeeAsync(id, trackChanges);

        if (projectManager is not null && projectManager.Id.Equals(id))
        {
            var projects = await _repository.Project.GetProjectsByEmployeeAsync(id, trackChanges);

            foreach (var project in projects)
            {
                var projectEmployees = await _repository.ProjectEmployee.GetProjectEmployeesByProjectIdAsync(project.Id, true);

                foreach (var pe in projectEmployees)
                {
                    if (pe.ProjectManagerId.Equals(id))
                    {
                        pe.ProjectManagerId = null;
                    }
                }
            }
        }

        _repository.Employee.DeleteEmployee(employee);
        await _repository.SaveAsync();
    }

    public async Task DeleteProjectFromEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employeeForProject = await _repository.Employee.GetEmployeeByProjectAsync(projectId, employeeId, trackChanges);

        if (employeeForProject is null)
            throw new EmployeeNotFoundException(employeeId);

        var projectManager = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectId, trackChanges);

        if (projectManager.Id.Equals(employeeId))
        {
            var projectEmployee = await _repository.ProjectEmployee.GetProjectEmployeeAsync(projectId, employeeId, trackChanges);
            _repository.ProjectEmployee.DeleteProjectEmployee(projectEmployee);
        }

        var projectEmployees = await _repository.ProjectEmployee.GetProjectEmployeesByProjectIdAsync(projectId, true);

        if (projectEmployees is not null)
        {
            foreach (var pe in projectEmployees)
            {
                pe.ProjectManagerId = null;
            }
        }

        await _repository.SaveAsync();
    }

    public async Task UpdateEmployeeForProjectAsync(Guid projectId, Guid employeeId, EmployeeForUpdateDto employeeForUpdate, bool projectTrackChanges, bool employeeTrackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(projectId, projectTrackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employeeEntity = await _repository.Employee.GetEmployeeAsync(employeeId, employeeTrackChanges);

        if (employeeEntity is null)
            throw new EmployeeNotFoundException(employeeId);

        _mapper.Map(employeeForUpdate, employeeEntity);
        await _repository.SaveAsync();
    }

    public async Task UpdateEmployeeAsync(Guid employeeId, EmployeeForUpdateDto employeeForUpdate, bool trackChanges)
    {
        var employeeEntity = await _repository.Employee.GetEmployeeAsync(employeeId, trackChanges);

        if (employeeEntity is null)
            throw new EmployeeNotFoundException(employeeId);

        _mapper.Map(employeeForUpdate, employeeEntity);
        await _repository.SaveAsync();
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid employeeId, bool trackChanges)
    {
        var employeeEntity = await _repository.Employee.GetEmployeeAsync(employeeId, trackChanges);

        if (employeeEntity is null)
            throw new EmployeeNotFoundException(employeeId);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
        return (employeeToPatch, employeeEntity);
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid projectId, Guid employeeId, bool projectTrackChanges, bool employeeTrackChanges)
    {
        var project = await _repository.Project.GetProjectAsync(projectId, projectTrackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employeeEntity = await _repository.Employee.GetEmployeeByProjectAsync(projectId, employeeId, employeeTrackChanges);

        if (employeeEntity is null)
            throw new EmployeeNotFoundException(employeeId);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
        return (employeeToPatch, employeeEntity);
    }

    public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
    {
        _mapper.Map(employeeToPatch, employeeEntity);
        await _repository.SaveAsync();
    }
}
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
        var employeesFromDb = await _repository.Employee.GetAllEmployeesAsync(trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
        return employeesDto;
    }

    public async Task<EmployeeDto> GetEmployeeAsync(Guid id, bool trackChanges)
    {
        var employeeDb = await GetEmployeeAndCheckIfItExists(id, trackChanges);
        var employeeDto = _mapper.Map<EmployeeDto>(employeeDb);
        return employeeDto;
    }

    public async Task<EmployeeDto> GetEmployeeByProjectAsync(Guid projectId, Guid employeeId, bool trackChanges)
    {
        await CheckIfProjectExists(projectId, trackChanges);
        var employeeDb = await GetEmployeeByProjectAndCheckIfItExists(projectId, employeeId, trackChanges);
        var employeeDto = _mapper.Map<EmployeeDto>(employeeDb);
        return employeeDto;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesByProjectAsync(Guid projectId, bool trackChanges)
    {
        await CheckIfProjectExists(projectId, trackChanges);
        var employeesFromDb = await _repository.Employee.GetEmployeesByProjectAsync(projectId, trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
        return employeesDto;
    }

    public async Task<EmployeeDto> GetProjectManagerByProjectAsync(Guid projectId, bool trackChanges)
    {
        await CheckIfProjectExists(projectId, trackChanges);

        var projectManagerDb = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectId, trackChanges);
        if (projectManagerDb is null)
            throw new ProjectManagerNotFoundException();

        var projectManagerDto = _mapper.Map<EmployeeDto>(projectManagerDb);
        return projectManagerDto;
    }

    public async Task<EmployeeDto> GetProjectManagerByEmployeeAsync(Guid employeeId, bool trackChanges)
    {
        _ = await GetEmployeeAndCheckIfItExists(employeeId, trackChanges);

        var projectManagerDb = await _repository.ProjectEmployee.GetProjectManagerByEmployeeAsync(employeeId, trackChanges);

        if (projectManagerDb is null)
            throw new ProjectManagerNotFoundException();

        var projectManagerDto = _mapper.Map<EmployeeDto>(projectManagerDb);
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
        await CheckIfProjectExists(projectId, trackChanges);

        var employeeEntity = _mapper.Map<Employee>(employeeFoCreation);
        _repository.Employee.CreateEmployee(employeeEntity);

        var projectManagerDb = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectId, trackChanges);
        _repository.ProjectEmployee.CreateProjectEmployee(projectId, employeeEntity.Id, projectManagerDb.Id);

        await _repository.SaveAsync();

        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;
    }

    public async Task<EmployeeDto> CreateProjectManagerForProjectAsync(Guid projectId, EmployeeForCreationDto projectManagerForCreation, bool trackChanges)
    {
        await CheckIfProjectExists(projectId, trackChanges);

        var projectManagerEntity = _mapper.Map<Employee>(projectManagerForCreation);

        _repository.Employee.CreateEmployee(projectManagerEntity);
        _repository.ProjectEmployee.CreateProjectEmployee(projectId, projectManagerEntity.Id, projectManagerEntity.Id);

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
        var employeesFromDb = await _repository.Employee.GetEmployeesWithoutProjectAsync(trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
        return employeesDto;
    }

    public async Task DeleteEmployeeAsync(Guid id, bool trackChanges)
    {
        var employeeDb = await GetEmployeeAndCheckIfItExists(id, trackChanges);

        var projectManagerDb = await _repository.ProjectEmployee.GetProjectManagerByEmployeeAsync(id, trackChanges);

        if (projectManagerDb is not null && projectManagerDb.Id.Equals(id))
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

        _repository.Employee.DeleteEmployee(employeeDb);
        await _repository.SaveAsync();
    }

    public async Task DeleteProjectFromEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges)
    {
        await CheckIfProjectExists(projectId, trackChanges);
        _ = await GetEmployeeByProjectAndCheckIfItExists(projectId, employeeId, trackChanges);

        var projectManagerDb = await _repository.ProjectEmployee.GetProjectManagerByProjectAsync(projectId, trackChanges);

        if (projectManagerDb.Id.Equals(employeeId))
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
        await CheckIfProjectExists(projectId, projectTrackChanges);
        var employeeDb = await GetEmployeeByProjectAndCheckIfItExists(projectId, employeeId, employeeTrackChanges);
        _mapper.Map(employeeForUpdate, employeeDb);
        await _repository.SaveAsync();
    }

    public async Task UpdateEmployeeAsync(Guid employeeId, EmployeeForUpdateDto employeeForUpdate, bool trackChanges)
    {
        var employeeDb = await GetEmployeeAndCheckIfItExists(employeeId, trackChanges);
        _mapper.Map(employeeForUpdate, employeeDb);
        await _repository.SaveAsync();
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid employeeId, bool trackChanges)
    {
        var employeeDb = await GetEmployeeAndCheckIfItExists(employeeId, trackChanges);
        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeDb);
        return (employeeToPatch, employeeDb);
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeByProjectForPatchAsync(Guid projectId, Guid employeeId, bool projectTrackChanges, bool employeeTrackChanges)
    {
        await CheckIfProjectExists(projectId, projectTrackChanges);
        var employeeDb = await GetEmployeeByProjectAndCheckIfItExists(projectId, employeeId, employeeTrackChanges);
        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeDb);
        return (employeeToPatch, employeeDb);
    }

    public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
    {
        _mapper.Map(employeeToPatch, employeeEntity);
        await _repository.SaveAsync();
    }

    private async Task<Employee> GetEmployeeAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var employeeDb = await _repository.Employee.GetEmployeeAsync(id, trackChanges);
        if (employeeDb is null)
            throw new EmployeeNotFoundException(id);
        return employeeDb;
    }

    private async Task CheckIfProjectExists(Guid id, bool trackChanges)
    {
        var projectDb = await _repository.Project.GetProjectAsync(id, trackChanges);
        if (projectDb is null)
            throw new ProjectNotFoundException(id);
    }

    private async Task<Employee> GetEmployeeByProjectAndCheckIfItExists(Guid projectId, Guid employeeId, bool trackChanges)
    {
        var employeeDb = await _repository.Employee.GetEmployeeByProjectAsync(projectId, employeeId, trackChanges);
        if (employeeDb is null)
            throw new EmployeeNotFoundException(employeeId);
        return employeeDb;
    }
}
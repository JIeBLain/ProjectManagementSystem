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

    public IEnumerable<EmployeeDto> GetAllEmployees(bool trackChanges)
    {
        var employees = _repository.Employee.GetAllEmployees(trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeesDto;
    }

    public EmployeeDto GetEmployee(Guid id, bool trackChanges)
    {
        var employee = _repository.Employee.GetEmployee(id, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(id);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }

    public EmployeeDto GetEmployeeByProject(Guid projectId, Guid employeeId, bool trackChanges)
    {
        var project = _repository.Project.GetProject(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employee = _repository.Employee.GetEmployee(employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }

    public IEnumerable<EmployeeDto> GetEmployeesByProject(Guid projectId, bool trackChanges)
    {
        var project = _repository.Project.GetProject(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employees = _repository.Employee.GetEmployeesByProject(projectId, trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeesDto;
    }

    public EmployeeDto GetProjectManager(Guid projectId, bool trackChanges)
    {
        var project = _repository.Project.GetProject(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var projectManager = _repository.Employee.GetProjectManager(projectId, trackChanges);
        var projectManagerDto = _mapper.Map<EmployeeDto>(projectManager);
        return projectManagerDto;
    }

    public EmployeeDto CreateEmployee(EmployeeForCreationDto employee)
    {
        var employeeEntity = _mapper.Map<Employee>(employee);

        _repository.Employee.CreateEmployee(employeeEntity);
        _repository.Save();

        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;
    }

    public EmployeeDto CreateEmployeeForProject(Guid projectId, EmployeeForCreationDto employeeFoCreation, bool trackChanges)
    {
        var project = _repository.Project.GetProject(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var employeeEntity = _mapper.Map<Employee>(employeeFoCreation);

        _repository.Employee.CreateEmployeeForProject(projectId, employeeEntity);
        _repository.Save();

        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;
    }

    public EmployeeDto CreateProjectManagerForProject(Guid projectId, EmployeeForCreationDto projectManagerForCreation, bool trackChanges)
    {
        var project = _repository.Project.GetProject(projectId, trackChanges);

        if (project is null)
            throw new ProjectNotFoundException(projectId);

        var projectManagerEntity = _mapper.Map<Employee>(projectManagerForCreation);

        _repository.Employee.CreateProjectManagerForProject(projectId, projectManagerEntity);
        _repository.Save();

        var projectManagerToReturn = _mapper.Map<EmployeeDto>(projectManagerEntity);
        return projectManagerToReturn;
    }

    public IEnumerable<EmployeeDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var employeeEntities = _repository.Employee.GetByIds(ids, trackChanges);

        if (ids.Count() != employeeEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
        return employeesToReturn;
    }

    public (IEnumerable<EmployeeDto> employees, string ids) CreateEmployeeCollection(IEnumerable<EmployeeForCreationDto> employeeCollection)
    {
        if (employeeCollection is null)
            throw new EmployeeCollectionBadRequest();

        var employeeEntities = _mapper.Map<IEnumerable<Employee>>(employeeCollection);

        foreach (var employee in employeeEntities)
        {
            _repository.Employee.CreateEmployee(employee);
        }

        _repository.Save();

        var employeeCollectionToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
        var ids = string.Join(",", employeeCollectionToReturn.Select(e => e.Id));
        return (employees: employeeCollectionToReturn, ids);
    }

    public IEnumerable<EmployeeDto> GetEmployeesWithoutProject(bool trackChanges)
    {
        var employees = _repository.Employee.GetEmployeesWithoutProject(trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeesDto;
    }
}
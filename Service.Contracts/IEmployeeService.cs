using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetAllEmployees(bool trackChanges);
    EmployeeDto GetEmployee(Guid id, bool trackChanges);
    IEnumerable<EmployeeDto> GetEmployeesByProject(Guid projectId, bool trackChanges);
    EmployeeDto GetEmployeeByProject(Guid projectId, Guid employeeId, bool trackChanges);
    EmployeeDto GetProjectManager(Guid projectId, bool trackChanges);
    EmployeeDto CreateEmployee(EmployeeForCreationDto employee);
    EmployeeDto CreateEmployeeForProject(Guid projectId, EmployeeForCreationDto employeeFoCreation, bool trackChanges);
    EmployeeDto CreateProjectManagerForProject(Guid projectId, EmployeeForCreationDto projectManagerForCreation, bool trackChanges);
    IEnumerable<EmployeeDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    (IEnumerable<EmployeeDto> employees, string ids) CreateEmployeeCollection(IEnumerable<EmployeeForCreationDto> employeeCollection);
    IEnumerable<EmployeeDto> GetEmployeesWithoutProject(bool trackChanges);
    void DeleteEmployee(Guid id, bool trackChanges);
    void DeleteEmployeeForProject(Guid projectId, Guid employeeId, bool trackChanges);
}
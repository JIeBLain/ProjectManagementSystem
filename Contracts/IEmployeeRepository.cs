using Entities.Models;

namespace Contracts;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAllEmployees(bool trackChanges);
    Employee GetEmployee(Guid employeeId, bool trackChanges);
    IEnumerable<Employee> GetEmployeesByProject(Guid projectId, bool trackChanges);
    Employee GetEmployeeByProject(Guid projectId, Guid employeeId, bool trackChanges);
    Employee GetProjectManager(Guid projectId, bool trackChanges);
    void CreateEmployee(Employee employee);
    void CreateEmployeeForProject(Guid projectId, Employee employee);
    void CreateProjectManagerForProject(Guid projectId, Employee projectManager);
    IEnumerable<Employee> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    IEnumerable<Employee> GetEmployeesWithoutProject(bool trackChanges);
    void DeleteEmployeeForProject(Guid projectId, Guid employeeId, bool trackChanges);
}
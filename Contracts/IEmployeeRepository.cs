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
}
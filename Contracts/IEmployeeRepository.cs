using Entities.Models;

namespace Contracts;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges);
    Task<Employee> GetEmployeeAsync(Guid employeeId, bool trackChanges);
    Task<IEnumerable<Employee>> GetEmployeesByProjectAsync(Guid projectId, bool trackChanges);
    Task<Employee> GetEmployeeByProjectAsync(Guid projectId, Guid employeeId, bool trackChanges);
    void CreateEmployee(Employee employee);
    Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<IEnumerable<Employee>> GetEmployeesWithoutProjectAsync(bool trackChanges);
    void DeleteEmployee(Employee employee);
}
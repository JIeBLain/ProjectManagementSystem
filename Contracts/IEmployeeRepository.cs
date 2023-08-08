using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IEmployeeRepository
{
    Task<PagedList<Employee>> GetAllEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges);
    Task<Employee> GetEmployeeAsync(Guid employeeId, bool trackChanges);
    Task<PagedList<Employee>> GetEmployeesByProjectAsync(Guid projectId, EmployeeParameters employeeParameters, bool trackChanges);
    Task<Employee> GetEmployeeByProjectAsync(Guid projectId, Guid employeeId, bool trackChanges);
    void CreateEmployee(Employee employee);
    Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<PagedList<Employee>> GetEmployeesWithoutProjectAsync(EmployeeParameters employeeParameters, bool trackChanges);
    void DeleteEmployee(Employee employee);
}
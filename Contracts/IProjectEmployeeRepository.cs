using Entities.Models;

namespace Contracts;

public interface IProjectEmployeeRepository
{
    Task<IEnumerable<ProjectEmployee>> GetAllProjectEmployeesAsync(bool trackChanges);
    Task<ProjectEmployee> GetProjectEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges);
    Task<IEnumerable<ProjectEmployee>> GetProjectEmployeesByProjectIdAsync(Guid projectId, bool trackChanges);
    Task<IEnumerable<ProjectEmployee>> GetProjectEmployeesByEmployeeIdAsync(Guid employeeId, bool trackChanges);
    Task<Employee> GetProjectManagerByProjectAsync(Guid projectId, bool trackChanges);
    Task<Employee> GetProjectManagerByEmployeeAsync(Guid employeeId, bool trackChanges);
    void CreateProjectEmployee(Guid projectId, Guid employeeId, Guid? projectManagerId);
    void DeleteProjectEmployee(ProjectEmployee projectEmployee);
}
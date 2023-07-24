using Entities.Models;

namespace Contracts;

public interface IProjectEmployeeRepository
{
    ProjectEmployee GetProjectEmployee(Guid projectId, Guid employeeId, bool trackChanges);
    IEnumerable<ProjectEmployee> GetProjectEmployeesByProjectId(Guid projectId, bool trackChanges);
    IEnumerable<ProjectEmployee> GetProjectEmployeesByEmployeeId(Guid employeeId, bool trackChanges);
    Employee GetProjectManagerByProject(Guid projectId, bool trackChanges);
    Employee GetProjectManagerByEmployee(Guid employeeId, bool trackChanges);
    void CreateProjectEmployee(Guid projectId, Guid employeeId, Guid? projectManagerId);
    void DeleteProjectEmployee(ProjectEmployee projectEmployee);
}
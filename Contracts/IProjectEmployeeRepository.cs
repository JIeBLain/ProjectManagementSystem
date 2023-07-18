using Entities.Models;

namespace Contracts;

public interface IProjectEmployeeRepository
{
    ProjectEmployee GetProjectEmployee(Guid projectId, Guid employeeId, bool trackChanges);
    Employee GetProjectManagerByProject(Guid projectId, bool trackChanges);
    Employee GetProjectManagerByEmployee(Guid employeeId, bool trackChanges);
    void CreateProjectEmployee(Project project, Employee employee);
    void CreateProjectManagerForProject(Guid projectId, Employee projectManager);
    void DeleteProjectEmployee(ProjectEmployee projectEmployee);
}
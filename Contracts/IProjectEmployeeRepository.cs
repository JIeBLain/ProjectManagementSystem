using Entities.Models;

namespace Contracts;

public interface IProjectEmployeeRepository
{
    ProjectEmployee GetProjectEmployee(Guid projectId, Guid employeeId, bool trackChanges);
    void CreateProjectEmployee(Project project, Employee employee);
    void DeleteProjectEmployee(ProjectEmployee projectEmployee);
}
using Entities.Models;

namespace Contracts;

public interface IProjectRepository
{
    IEnumerable<Project> GetAllProjects(bool trackChanges);
    Project GetProject(Guid projectId, bool trackChanges);
    IEnumerable<Project> GetProjectsByEmployee(Guid employeeId, bool trackChanges);
    Project GetProjectByEmployee(Guid employeeId, Guid projectId, bool trackChanges);
    void CreateProject(Project project);
    void CreateProjectForEmployee(Guid employeeId, Project project);
    IEnumerable<Project> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteProject(Project project);
}
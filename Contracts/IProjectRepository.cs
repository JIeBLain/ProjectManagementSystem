using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IProjectRepository
{
    Task<PagedList<Project>> GetAllProjectsAsync(ProjectParameters projectParameters, bool trackChanges);
    Task<Project> GetProjectAsync(Guid projectId, bool trackChanges);
    Task<IEnumerable<Project>> GetProjectsByEmployeeAsync(Guid employeeId, bool trackChanges);
    Task<Project> GetProjectByEmployeeAsync(Guid employeeId, Guid projectId, bool trackChanges);
    void CreateProject(Project project);
    Task<IEnumerable<Project>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteProject(Project project);
}
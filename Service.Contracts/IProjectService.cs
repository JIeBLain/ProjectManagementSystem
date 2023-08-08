using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IProjectService
{
    Task<(IEnumerable<ProjectDto> projects, MetaData metaData)> GetAllProjectsAsync(ProjectParameters projectParameters, bool trackChanges);
    Task<ProjectDto> GetProjectAsync(Guid id, bool trackChanges);
    Task<IEnumerable<ProjectDto>> GetProjectsByEmployeeAsync(Guid employeeId, bool trackChanges);
    Task<ProjectDto> GetProjectByEmployeeAsync(Guid employeeId, Guid projectId, bool trackChanges);
    Task<ProjectDto> CreateProjectAsync(ProjectForCreationDto project);
    Task<IEnumerable<ProjectDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<ProjectDto> projects, string ids)> CreateProjectCollectionAsync(IEnumerable<ProjectForCreationDto> projectCollection);
    Task DeleteProjectAsync(Guid id, bool trackChanges);
    Task DeleteEmployeeFromProjectAsync(Guid employeeId, Guid projectId, bool trackChanges);
    Task UpdateProjectAsync(Guid projectId, ProjectForUpdateDto projectForUpdate, bool trackChanges);
    Task<(ProjectForUpdateDto projectToPatch, Project projectEntity)> GetProjectForPatchAsync(Guid projectId, bool trackChanges);
    Task SaveChangesForPatchAsync(ProjectForUpdateDto projectToPatch, Project projectEntity);
}
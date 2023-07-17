using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProjectService
{
    IEnumerable<ProjectDto> GetAllProjects(bool trackChanges);
    ProjectDto GetProject(Guid id, bool trackChanges);
    IEnumerable<ProjectDto> GetProjectsByEmployee(Guid employeeId, bool trackChanges);
    ProjectDto GetProjectByEmployee(Guid employeeId, Guid projectId, bool trackChanges);
    ProjectDto CreateProject(ProjectForCreationDto project);
    ProjectDto CreateProjectForEmployee(Guid employeeId, ProjectForCreationDto projectForCreation, bool trackChanges);
    IEnumerable<ProjectDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    (IEnumerable<ProjectDto> projects, string ids) CreateProjectCollection(IEnumerable<ProjectForCreationDto> projectCollection);
    void DeleteProject(Guid id, bool trackChanges);
}
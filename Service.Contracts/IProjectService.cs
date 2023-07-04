using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProjectService
{
    IEnumerable<ProjectDto> GetAllProjects(bool trackChanges);
    ProjectDto GetProject(Guid id, bool trackChanges);
    IEnumerable<ProjectDto> GetProjects(Guid employeeId, bool trackChanges);
}
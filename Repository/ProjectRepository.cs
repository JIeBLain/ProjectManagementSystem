using Contracts;
using Entities.Models;

namespace Repository;

public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public ProjectRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Project> GetAllProjects(bool trackChanges)
    {
        return FindAll(trackChanges)
            .OrderBy(p => p.Name)
            .ToList();
    }

    public Project GetProject(Guid projectId, bool trackChanges)
    {
        return FindByCondition(p => p.Id.Equals(projectId), trackChanges)
            .SingleOrDefault();
    }

    public Project GetProjectByEmployee(Guid employeeId, Guid projectId, bool trackChanges)
    {
        return FindByCondition(p => p.ProjectEmployees.Any(pe => pe.EmployeeId.Equals(employeeId)), trackChanges)
            .Where(p => p.Id.Equals(projectId))
            .SingleOrDefault();
    }

    public IEnumerable<Project> GetProjectsByEmployee(Guid employeeId, bool trackChanges)
    {
        return FindByCondition(p => p.ProjectEmployees.Any(pe => pe.EmployeeId.Equals(employeeId)), trackChanges)
            .OrderBy(p => p.Name)
            .ToList();
    }
}
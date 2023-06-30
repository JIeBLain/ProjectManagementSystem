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
}
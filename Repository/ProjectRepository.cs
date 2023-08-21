using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public ProjectRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Project>> GetAllProjectsAsync(ProjectParameters projectParameters, bool trackChanges)
    {
        var projects = await FindAll(trackChanges)
            .FilterProjects(projectParameters.MinPriority, projectParameters.MaxPriority)
            .Search(projectParameters.SearchTerm)
            .OrderBy(p => p.Name)
            .ToListAsync();

        return PagedList<Project>
            .ToPagedList(projects, projectParameters.PageNumber, projectParameters.PageSize);
    }

    public async Task<Project> GetProjectAsync(Guid projectId, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(projectId), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<Project> GetProjectByEmployeeAsync(Guid employeeId, Guid projectId, bool trackChanges)
    {
        return await FindByCondition(p => p.ProjectEmployees.Any(pe => pe.EmployeeId.Equals(employeeId)), trackChanges)
            .SingleOrDefaultAsync(p => p.Id.Equals(projectId));
    }

    public async Task<IEnumerable<Project>> GetProjectsByEmployeeAsync(Guid employeeId, bool trackChanges)
    {
        return await FindByCondition(p => p.ProjectEmployees.Any(pe => pe.EmployeeId.Equals(employeeId)), trackChanges)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public void CreateProject(Project project)
    {
        Create(project);
    }

    public async Task<IEnumerable<Project>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        return await FindByCondition(p => ids.Contains(p.Id), trackChanges)
            .ToListAsync();
    }

    public void DeleteProject(Project project)
    {
        Delete(project);
    }
}
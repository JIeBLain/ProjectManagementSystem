using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository;

public class ProjectEmployeeRepository : RepositoryBase<ProjectEmployee>, IProjectEmployeeRepository
{
    public ProjectEmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<ProjectEmployee>> GetAllProjectEmployeesAsync(ProjectEmployeeParameters projectEmployeeParameters, bool trackChanges)
    {
        var projectEmployees = await FindAll(trackChanges)
            .Include(pe => pe.Project)
            .Include(pe => pe.Employee)
            .Include(pe => pe.ProjectManager)
            .ToListAsync();

        return PagedList<ProjectEmployee>
            .ToPagedList(projectEmployees, projectEmployeeParameters.PageNumber, projectEmployeeParameters.PageSize);
    }

    public async Task<ProjectEmployee> GetProjectEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges)
    {
        return await FindByCondition(pe => pe.ProjectId.Equals(projectId) && pe.EmployeeId.Equals(employeeId), trackChanges)
            .Include(pe => pe.Project)
            .Include(pe => pe.Employee)
            .Include(pe => pe.ProjectManager)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<ProjectEmployee>> GetProjectEmployeesByProjectIdAsync(Guid projectId, bool trackChanges)
    {
        return await FindByCondition(pe => pe.ProjectId.Equals(projectId), trackChanges)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProjectEmployee>> GetProjectEmployeesByEmployeeIdAsync(Guid employeeId, bool trackChanges)
    {
        return await FindByCondition(pe => pe.EmployeeId.Equals(employeeId), trackChanges)
            .ToListAsync();
    }

    public async Task<Employee> GetProjectManagerByProjectAsync(Guid projectId, bool trackChanges)
    {
        return await FindByCondition(pe => pe.ProjectId.Equals(projectId), trackChanges)
            .Select(pe => pe.ProjectManager)
            .FirstOrDefaultAsync();
    }

    public async Task<Employee> GetProjectManagerByEmployeeAsync(Guid employeeId, bool trackChanges)
    {
        return await FindByCondition(pe => pe.EmployeeId.Equals(employeeId), trackChanges)
            .Select(pe => pe.ProjectManager)
            .FirstOrDefaultAsync();
    }

    public void CreateProjectEmployee(Guid projectId, Guid employeeId, Guid? projectManagerId = null)
    {
        var projectEmployee = new ProjectEmployee
        {
            ProjectId = projectId,
            EmployeeId = employeeId,
            ProjectManagerId = projectManagerId
        };

        Create(projectEmployee);
    }

    public void DeleteProjectEmployee(ProjectEmployee projectEmployee)
    {
        Delete(projectEmployee);
    }
}
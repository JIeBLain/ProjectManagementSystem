using Contracts;
using Entities.Models;

namespace Repository;

public class ProjectEmployeeRepository : RepositoryBase<ProjectEmployee>, IProjectEmployeeRepository
{
    public ProjectEmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public ProjectEmployee GetProjectEmployee(Guid projectId, Guid employeeId, bool trackChanges)
    {
        return FindByCondition(pe => pe.ProjectId.Equals(projectId) && pe.EmployeeId.Equals(employeeId), trackChanges)
            .SingleOrDefault();
    }

    public IEnumerable<ProjectEmployee> GetProjectEmployeesByProjectId(Guid projectId, bool trackChanges)
    {
        return FindByCondition(pe => pe.ProjectId.Equals(projectId), trackChanges)
            .ToList();
    }

    public IEnumerable<ProjectEmployee> GetProjectEmployeesByEmployeeId(Guid employeeId, bool trackChanges)
    {
        return FindByCondition(pe => pe.EmployeeId.Equals(employeeId), trackChanges)
            .ToList();
    }

    public Employee GetProjectManagerByProject(Guid projectId, bool trackChanges)
    {
        return FindByCondition(pe => pe.ProjectId.Equals(projectId), trackChanges)
            .Select(pe => pe.ProjectManager)
            .FirstOrDefault();
    }

    public Employee GetProjectManagerByEmployee(Guid employeeId, bool trackChanges)
    {
        return FindByCondition(pe => pe.EmployeeId.Equals(employeeId), trackChanges)
            .Select(pe => pe.ProjectManager)
            .FirstOrDefault();
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
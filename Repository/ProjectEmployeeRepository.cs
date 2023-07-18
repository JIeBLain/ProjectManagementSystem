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

    public Employee GetProjectManagerByProject(Guid projectId, bool trackChanges)
    {
        return FindByCondition(pe => pe.ProjectId.Equals(projectId), trackChanges)
            .Select(pe => pe.ProjectManager)
            .SingleOrDefault();
    }

    public Employee GetProjectManagerByEmployee(Guid employeeId, bool trackChanges)
    {
        return FindByCondition(pe => pe.EmployeeId.Equals(employeeId), trackChanges)
            .Select(pe => pe.ProjectManager)
            .SingleOrDefault();
    }

    public void CreateProjectEmployee(Project project, Employee employee)
    {
        var projectEmployee = new ProjectEmployee
        {
            ProjectId = project.Id,
            Project = project,
            EmployeeId = employee.Id,
            Employee = employee
        };

        Create(projectEmployee);
    }

    public void CreateProjectManagerForProject(Guid projectId, Employee projectManager)
    {
        var projectEmployees = RepositoryContext.ProjectEmployees
            .Where(pe => pe.ProjectId.Equals(projectId));

        foreach (var pe in projectEmployees)
        {
            pe.ProjectManager = projectManager;
        }

        var project = RepositoryContext.Projects
            .SingleOrDefault(p => p.Id.Equals(projectId));

        var projectEmployee = new ProjectEmployee
        {
            Project = project,
            Employee = projectManager,
            ProjectManager = projectManager
        };

        Create(projectEmployee);
    }

    public void DeleteProjectEmployee(ProjectEmployee projectEmployee)
    {
        Delete(projectEmployee);
    }
}
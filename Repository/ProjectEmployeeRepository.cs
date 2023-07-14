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

    public void DeleteProjectEmployee(ProjectEmployee projectEmployee)
    {
        Delete(projectEmployee);
    }
}
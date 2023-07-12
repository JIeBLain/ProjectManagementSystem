using Contracts;
using Entities.Models;

namespace Repository;

public class ProjectEmployeeRepository : RepositoryBase<ProjectEmployee>, IProjectEmployeeRepository
{
    public ProjectEmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
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
}
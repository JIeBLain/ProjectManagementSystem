using Contracts;
using Entities.Models;

namespace Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Employee> GetAllEmployees(bool trackChanges)
    {
        return FindAll(trackChanges)
            .OrderBy(e => e.LastName)
            .ToList();
    }

    public Employee GetEmployee(Guid employeeId, bool trackChanges)
    {
        return FindByCondition(e => e.Id.Equals(employeeId), trackChanges)
            .SingleOrDefault();
    }

    public Employee GetEmployeeByProject(Guid projectId, Guid employeeId, bool trackChanges)
    {
        return FindByCondition(e => e.ProjectEmployees.Any(pe => pe.ProjectId.Equals(projectId)), trackChanges)
            .Where(e => e.Id.Equals(employeeId))
            .SingleOrDefault();
    }

    public IEnumerable<Employee> GetEmployeesByProject(Guid projectId, bool trackChanges)
    {
        return FindByCondition(e => e.ProjectEmployees.Any(pe => pe.ProjectId.Equals(projectId)), trackChanges)
            .OrderBy(e => e.LastName)
            .ToList();
    }

    public Employee GetProjectManager(Guid projectId, bool trackChanges)
    {
        return FindByCondition(e => e.ProjectManagerProjects.Any(p => p.Id.Equals(projectId)), trackChanges)
             .SingleOrDefault();
    }

    public void CreateEmployee(Employee employee)
    {
        Create(employee);
    }

    public void CreateEmployeeForProject(Guid projectId, Employee employee)
    {
        var project = RepositoryContext.Projects
            .Where(p => p.Id.Equals(projectId))
            .SingleOrDefault();

        var projectEmployee = new ProjectEmployee { Project = project, Employee = employee };

        RepositoryContext.Add(projectEmployee);
        Create(employee);
    }

    public void CreateProjectManagerForProject(Guid projectId, Employee projectManager)
    {
        var project = RepositoryContext.Projects
            .Where(p => p.Id == projectId)
            .SingleOrDefault();

        project.ProjectManager = projectManager;

        var projectEmployee = new ProjectEmployee { Project = project, Employee = projectManager };

        RepositoryContext.Update(project);
        RepositoryContext.Add(projectEmployee);
        Create(projectManager);
    }
}
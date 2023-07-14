using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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
            .SingleOrDefault(e => e.Id.Equals(employeeId));
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
            .SingleOrDefault(p => p.Id.Equals(projectId));

        var projectEmployee = new ProjectEmployee { Project = project, Employee = employee };

        RepositoryContext.Add(projectEmployee);
        Create(employee);
    }

    public void CreateProjectManagerForProject(Guid projectId, Employee projectManager)
    {
        var project = RepositoryContext.Projects
            .SingleOrDefault(p => p.Id.Equals(projectId));

        project.ProjectManager = projectManager;

        var projectEmployee = new ProjectEmployee { Project = project, Employee = projectManager };

        RepositoryContext.Add(projectEmployee);
        Create(projectManager);
    }

    public IEnumerable<Employee> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        return FindByCondition(e => ids.Contains(e.Id), trackChanges)
            .ToList();
    }

    public IEnumerable<Employee> GetEmployeesWithoutProject(bool trackChanges)
    {
        return FindByCondition(e => e.ProjectEmployees.Count() == 0, trackChanges)
            .OrderBy(e => e.LastName)
            .ToList();
    }

    public void DeleteEmployeeForProject(Guid projectId, Guid employeeId, bool trackChanges)
    {

        var project = RepositoryContext.Projects
            .Include(p => p.ProjectManager)
            .Include(p => p.ProjectEmployees)
            .SingleOrDefault(p => p.Id.Equals(projectId));

        if (project.ProjectManager.Id.Equals(employeeId))
        {
            project.ProjectManager = null;
        }

        var projectEmployee = project.ProjectEmployees
            .SingleOrDefault(pe => pe.EmployeeId.Equals(employeeId));

        RepositoryContext.ProjectEmployees.Remove(projectEmployee);
    }
}
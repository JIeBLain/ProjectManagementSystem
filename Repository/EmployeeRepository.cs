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
            .SingleOrDefault(e => e.Id.Equals(employeeId));
    }

    public IEnumerable<Employee> GetEmployeesByProject(Guid projectId, bool trackChanges)
    {
        return FindByCondition(e => e.ProjectEmployees.Any(pe => pe.ProjectId.Equals(projectId)), trackChanges)
            .OrderBy(e => e.LastName)
            .ToList();
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

    public void DeleteEmployee(Employee employee)
    {
        Delete(employee);
    }

    public void DeleteEmployeeForProject(Guid projectId, Guid employeeId, bool trackChanges)
    {
        var projectEmployee = RepositoryContext.ProjectEmployees
            .SingleOrDefault(pe => pe.ProjectId.Equals(projectId) && pe.EmployeeId.Equals(employeeId));

        if (projectEmployee.ProjectManager.Id.Equals(employeeId))
        {
            var projectEmployees = RepositoryContext.ProjectEmployees.Where(pe => pe.ProjectId.Equals(projectId));

            foreach (var pe in projectEmployees)
            {
                pe.ProjectManager = null;
            }
        }

        RepositoryContext.ProjectEmployees.Remove(projectEmployee);
    }
}
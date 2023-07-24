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
}
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(e => e.LastName)
            .ToListAsync();
    }

    public async Task<Employee> GetEmployeeAsync(Guid employeeId, bool trackChanges)
    {
        return await FindByCondition(e => e.Id.Equals(employeeId), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<Employee> GetEmployeeByProjectAsync(Guid projectId, Guid employeeId, bool trackChanges)
    {
        return await FindByCondition(e => e.ProjectEmployees.Any(pe => pe.ProjectId.Equals(projectId)), trackChanges)
            .SingleOrDefaultAsync(e => e.Id.Equals(employeeId));
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByProjectAsync(Guid projectId, bool trackChanges)
    {
        return await FindByCondition(e => e.ProjectEmployees.Any(pe => pe.ProjectId.Equals(projectId)), trackChanges)
            .OrderBy(e => e.LastName)
            .ToListAsync();
    }

    public void CreateEmployee(Employee employee)
    {
        Create(employee);
    }

    public async Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        return await FindByCondition(e => ids.Contains(e.Id), trackChanges)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetEmployeesWithoutProjectAsync(bool trackChanges)
    {
        return await FindByCondition(e => e.ProjectEmployees.Count() == 0, trackChanges)
            .OrderBy(e => e.LastName)
            .ToListAsync();
    }

    public void DeleteEmployee(Employee employee)
    {
        Delete(employee);
    }
}
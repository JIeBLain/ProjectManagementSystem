using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.SupportingFunctionality;
using Shared.RequestFeatures;

namespace Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Employee>> GetAllEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges)
    {
        var gender = GenderParser.ConvertStringToGender(employeeParameters.Gender);

        var employees = await FindAll(trackChanges)
            .Where(e => string.IsNullOrEmpty(employeeParameters.Gender) || e.Gender.Equals(gender))
            .OrderBy(e => e.LastName)
            .ToListAsync();

        return PagedList<Employee>
            .ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
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

    public async Task<PagedList<Employee>> GetEmployeesByProjectAsync(Guid projectId, EmployeeParameters employeeParameters, bool trackChanges)
    {
        var gender = GenderParser.ConvertStringToGender(employeeParameters.Gender);

        var employees = await FindByCondition(e => e.ProjectEmployees.Any(pe => pe.ProjectId.Equals(projectId)), trackChanges)
            .Where(e => string.IsNullOrEmpty(employeeParameters.Gender) || e.Gender.Equals(gender))
            .OrderBy(e => e.LastName)
            .ToListAsync();

        return PagedList<Employee>
            .ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
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

    public async Task<PagedList<Employee>> GetEmployeesWithoutProjectAsync(EmployeeParameters employeeParameters, bool trackChanges)
    {
        var gender = GenderParser.ConvertStringToGender(employeeParameters.Gender);

        var employees = await FindByCondition(e => e.ProjectEmployees.Count() == 0, trackChanges)
            .Where(e => string.IsNullOrEmpty(employeeParameters.Gender) || e.Gender.Equals(gender))
            .OrderBy(e => e.LastName)
            .ToListAsync();

        return PagedList<Employee>
            .ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
    }

    public void DeleteEmployee(Employee employee)
    {
        Delete(employee);
    }
}
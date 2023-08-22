using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IEmployeeService
{
    Task<(IEnumerable<Entity> employees, MetaData metaData)> GetAllEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges);
    Task<EmployeeDto> GetEmployeeAsync(Guid id, bool trackChanges);
    Task<(IEnumerable<Entity> employees, MetaData metaData)> GetEmployeesByProjectAsync(Guid projectId, EmployeeParameters employeeParameters, bool trackChanges);
    Task<EmployeeDto> GetEmployeeByProjectAsync(Guid projectId, Guid employeeId, bool trackChanges);
    Task<EmployeeDto> GetProjectManagerByProjectAsync(Guid projectId, bool trackChanges);
    Task<EmployeeDto> GetProjectManagerByEmployeeAsync(Guid employeeId, bool trackChanges);
    Task<EmployeeDto> CreateEmployeeAsync(EmployeeForCreationDto employee);
    Task<EmployeeDto> CreateEmployeeForProjectAsync(Guid projectId, EmployeeForCreationDto employeeFoCreation, bool trackChanges);
    Task<EmployeeDto> CreateProjectManagerForProjectAsync(Guid projectId, EmployeeForCreationDto projectManagerForCreation, bool trackChanges);
    Task<IEnumerable<EmployeeDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<EmployeeDto> employees, string ids)> CreateEmployeeCollectionAsync(IEnumerable<EmployeeForCreationDto> employeeCollection);
    Task<(IEnumerable<Entity> employees, MetaData metaData)> GetEmployeesWithoutProjectAsync(EmployeeParameters employeeParameters, bool trackChanges);
    Task DeleteEmployeeAsync(Guid id, bool trackChanges);
    Task DeleteProjectFromEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges);
    Task UpdateEmployeeAsync(Guid employeeId, EmployeeForUpdateDto employeeForUpdate, bool trackChanges);
    Task UpdateEmployeeForProjectAsync(Guid projectId, Guid employeeId,
        EmployeeForUpdateDto employeeForUpdate, bool projectTrackChanges, bool employeeTrackChanges);
    Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid employeeId, bool trackChanges);
    Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeByProjectForPatchAsync(Guid projectId, Guid employeeId,
        bool projectTrackChanges, bool employeeTrackChanges);
    Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}
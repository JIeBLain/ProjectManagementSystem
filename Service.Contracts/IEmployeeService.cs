using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetAllEmployees(bool trackChanges);
    EmployeeDto GetEmployee(Guid id, bool trackChanges);
    IEnumerable<EmployeeDto> GetEmployeesByProject(Guid projectId, bool trackChanges);
    EmployeeDto GetEmployeeByProject(Guid projectId, Guid employeeId, bool trackChanges);
    EmployeeDto GetProjectManagerByProject(Guid projectId, bool trackChanges);
    EmployeeDto GetProjectManagerByEmployee(Guid employeeId, bool trackChanges);
    EmployeeDto CreateEmployee(EmployeeForCreationDto employee);
    EmployeeDto CreateEmployeeForProject(Guid projectId, EmployeeForCreationDto employeeFoCreation, bool trackChanges);
    EmployeeDto CreateProjectManagerForProject(Guid projectId, EmployeeForCreationDto projectManagerForCreation, bool trackChanges);
    IEnumerable<EmployeeDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    (IEnumerable<EmployeeDto> employees, string ids) CreateEmployeeCollection(IEnumerable<EmployeeForCreationDto> employeeCollection);
    IEnumerable<EmployeeDto> GetEmployeesWithoutProject(bool trackChanges);
    void DeleteEmployee(Guid id, bool trackChanges);
    void DeleteProjectFromEmployee(Guid projectId, Guid employeeId, bool trackChanges);
    void UpdateEmployee(Guid employeeId, EmployeeForUpdateDto employeeForUpdate, bool trackChanges);
    void UpdateEmployeeForProject(Guid projectId, Guid employeeId,
        EmployeeForUpdateDto employeeForUpdate, bool projectTrackChanges, bool employeeTrackChanges);
    (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(Guid employeeId, bool trackChanges);
    (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(Guid projectId, Guid employeeId,
        bool projectTrackChanges, bool employeeTrackChanges);
    void SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}
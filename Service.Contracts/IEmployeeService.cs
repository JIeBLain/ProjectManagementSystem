using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetAllEmployees(bool trackChanges);
    EmployeeDto GetEmployee(Guid id, bool trackChanges);
    IEnumerable<EmployeeDto> GetEmployeesByProject(Guid projectId, bool trackChanges);
    EmployeeDto GetEmployeeByProject(Guid projectId, Guid employeeId, bool trackChanges);
}
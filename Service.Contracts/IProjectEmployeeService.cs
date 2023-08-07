using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProjectEmployeeService
{
    Task<IEnumerable<ProjectEmployeeDto>> GetAllProjectEmployeesAsync(bool trackChanges);
    Task<ProjectEmployeeDto> GetProjectEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges);
    Task<ProjectEmployeeDto> CreateProjectEmployeeAsync(ProjectEmployeeForCreationDto projectEmployee, bool trackChanges);
}
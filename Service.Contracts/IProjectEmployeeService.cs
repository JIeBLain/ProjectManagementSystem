using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProjectEmployeeService
{
    IEnumerable<ProjectEmployeeDto> GetAllProjectEmployees(bool trackChanges);
    ProjectEmployeeDto GetProjectEmployee(Guid projectId, Guid employeeId, bool trackChanges);
    ProjectEmployeeDto CreateProjectEmployee(ProjectEmployeeForCreationDto projectEmployee, bool trackChanges);
}
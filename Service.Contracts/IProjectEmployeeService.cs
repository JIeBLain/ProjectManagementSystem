using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IProjectEmployeeService
{
    Task<(IEnumerable<ProjectEmployeeDto> projectEmployees, MetaData metaData)> GetAllProjectEmployeesAsync
        (ProjectEmployeeParameters projectEmployeeParameters, bool trackChanges);
    Task<ProjectEmployeeDto> GetProjectEmployeeAsync(Guid projectId, Guid employeeId, bool trackChanges);
    Task<ProjectEmployeeDto> CreateProjectEmployeeAsync(ProjectEmployeeForCreationDto projectEmployee, bool trackChanges);
}
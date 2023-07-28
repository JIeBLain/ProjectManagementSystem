namespace Shared.DataTransferObjects;

public record ProjectEmployeeDto(
    ProjectDto Project,
    EmployeeDto Employee,
    EmployeeDto? ProjectManager = null);
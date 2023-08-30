namespace Shared.DataTransferObjects;

public record ProjectEmployeeDto
{
    public ProjectDto? Project { get; init; }
    public EmployeeDto? Employee { get; init; }
    public EmployeeDto? ProjectManager { get; init; }
}
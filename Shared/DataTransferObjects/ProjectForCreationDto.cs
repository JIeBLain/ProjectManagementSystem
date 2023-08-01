namespace Shared.DataTransferObjects;

public record ProjectForCreationDto : ProjectForManipulationDto
{
    public EmployeeForCreationDto? ProjectManager { get; init; }
    public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
}
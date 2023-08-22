namespace Shared.DataTransferObjects;

public record EmployeeDto
{
    public Guid Id { get; init; }
    public string? FullName { get; init; }
    public DateTime BirthDate { get; init; }
    public string? Gender { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
}
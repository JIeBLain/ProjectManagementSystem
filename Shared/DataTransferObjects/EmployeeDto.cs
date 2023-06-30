namespace Shared.DataTransferObjects;

public record EmployeeDto(
    Guid Id,
    string FullName,
    string BirthDate,
    string Gender,
    string Email,
    string Phone);
namespace Shared.DataTransferObjects;

public record EmployeeDto(
    Guid Id,
    string FullName,
    DateTime BirthDate,
    string Gender,
    string Email,
    string Phone);
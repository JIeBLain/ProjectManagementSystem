namespace Shared.DataTransferObjects;

public record EmployeeDto(
    Guid Id,
    string FullName,
    int Age,
    string Gender,
    string Email,
    string Phone);
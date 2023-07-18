namespace Shared.DataTransferObjects;

public record EmployeeForUpdateDto(
    string FirstName,
    string LastName,
    string PatronymicName,
    DateTime BirthDate,
    string Gender,
    string Email,
    string Phone);
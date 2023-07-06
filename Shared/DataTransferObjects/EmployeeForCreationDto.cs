namespace Shared.DataTransferObjects;

public record EmployeeForCreationDto(
    string FirstName,
    string LastName,
    string PatronymicName,
    DateTime BirthDate,
    string Gender,
    string Email,
    string Phone);
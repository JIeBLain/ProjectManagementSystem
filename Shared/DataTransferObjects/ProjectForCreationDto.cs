namespace Shared.DataTransferObjects;

public record ProjectForCreationDto(
    string Name,
    string CustomerCompany,
    string ContractorCompany,
    DateTime StartTime,
    DateTime EndTime,
    int Priority,
    EmployeeForCreationDto ProjectManager,
    IEnumerable<EmployeeForCreationDto> Employees);
namespace Shared.DataTransferObjects;

public record ProjectForUpdateDto(
    string Name,
    string CustomerCompany,
    string ContractorCompany,
    DateTime StartTime,
    DateTime EndTime,
    int Priority);
namespace Shared.DataTransferObjects;

public record ProjectDto(
    Guid Id,
    string Name,
    string CustomerCompany,
    string ContractorCompany,
    string StartTime,
    string EndTime,
    int Priority);
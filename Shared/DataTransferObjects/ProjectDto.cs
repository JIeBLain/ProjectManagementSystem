namespace Shared.DataTransferObjects;

public record ProjectDto(
    Guid Id,
    string? Name,
    string? CustomerCompany,
    string? ContractorCompany,
    DateTime? StartTime,
    DateTime? EndTime,
    int Priority);
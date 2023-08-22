namespace Shared.DataTransferObjects;

public record ProjectDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? CustomerCompany { get; init; }
    public string? ContractorCompany { get; init; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public int Priority { get; init; }
}
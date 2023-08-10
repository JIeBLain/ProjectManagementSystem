namespace Shared.RequestFeatures;

public class ProjectParameters : RequestParameters
{
    public int MinPriority { get; set; } = 1;
    public int MaxPriority { get; set; } = 3;

    public bool ValidPriorityRange =>
        MaxPriority >= MinPriority
        && MinPriority >= 1 && MinPriority <= 3
        && MaxPriority <= 3 && MaxPriority >= 1;
}
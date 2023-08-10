namespace Entities.Exceptions;

public sealed class PriorityRangeBadRequestException : BadRequestException
{
    public PriorityRangeBadRequestException()
        : base("Priority can't be less than 1 or greater than 3.")
    {
    }
}
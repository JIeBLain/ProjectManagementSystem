namespace Entities.Exceptions;

public sealed class GenderBadRequestException : BadRequestException
{
    public GenderBadRequestException()
        : base("Write in the correct gender: male or female.")
    {
    }
}
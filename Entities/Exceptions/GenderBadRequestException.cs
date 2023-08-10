namespace Entities.Exceptions;

public class GenderBadRequestException : BadRequestException
{
    public GenderBadRequestException()
        : base("Write in the correct gender: male or female.")
    {
    }
}
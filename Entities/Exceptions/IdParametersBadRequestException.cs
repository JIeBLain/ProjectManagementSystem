namespace Entities.Exceptions;

public sealed class IdParametersBadRequestException : BadRequestException
{
    public IdParametersBadRequestException()
        : base("Collection count mismatch comparing to ids.")
    {
    }
}
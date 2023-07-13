namespace Entities.Exceptions;

public sealed class CollectionByIdsBadRequestException : BadRequestException
{
    public CollectionByIdsBadRequestException() : base("Parameter ids is null")
    {
    }
}
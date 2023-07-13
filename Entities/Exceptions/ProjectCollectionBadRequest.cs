namespace Entities.Exceptions;

public sealed class ProjectCollectionBadRequest : BadRequestException
{
    public ProjectCollectionBadRequest() : base("Project collection sent from a client is null.")
    {
    }
}
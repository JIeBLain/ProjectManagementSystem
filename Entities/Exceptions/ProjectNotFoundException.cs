namespace Entities.Exceptions;

public class ProjectNotFoundException : NotFoundException
{
    public ProjectNotFoundException(Guid projectId)
        : base($"The project with id: {projectId} doesn't exist in the database.")
    {
    }
}
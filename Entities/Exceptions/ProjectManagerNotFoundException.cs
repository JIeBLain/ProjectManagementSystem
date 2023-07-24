namespace Entities.Exceptions;
public sealed class ProjectManagerNotFoundException : NotFoundException
{
    public ProjectManagerNotFoundException() : base("The project manager doesn't exist in the database.")
    {
    }
}
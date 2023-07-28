namespace Entities.Exceptions;

public sealed class ProjectEmployeeNotFoundException : NotFoundException
{
    public ProjectEmployeeNotFoundException(Guid projectId, Guid employeeId)
        : base($"The project with id: {projectId} and employee with id: {employeeId} doesn't exist together in the database.")
    {
    }
}
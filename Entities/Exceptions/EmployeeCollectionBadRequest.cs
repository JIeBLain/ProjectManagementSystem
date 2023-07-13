namespace Entities.Exceptions;

public sealed class EmployeeCollectionBadRequest : BadRequestException
{
    public EmployeeCollectionBadRequest()
        : base("Employee collection sent from a client is null.")
    {
    }
}
namespace Shared.DataTransferObjects;

public record ProjectEmployeeForCreationDto(Guid ProjectId, Guid EmployeeId, Guid? ProjectManagerId);
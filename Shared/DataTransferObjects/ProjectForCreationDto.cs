using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ProjectForCreationDto
{
    [Required(ErrorMessage = "Project name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "The name of the customer's company is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the CustomerCompany is 60 characters.")]
    public string? CustomerCompany { get; init; }

    [Required(ErrorMessage = "The name of the contractor's company is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the ContractorCompany is 60 characters.")]
    public string? ContractorCompany { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }

    [Required(ErrorMessage = "Priority is a required field.")]
    [Range(1, 3, ErrorMessage = "The priority should range from 1 to 3, with 1 being low, 2 medium, and 3 high.")]
    public int Priority { get; init; }
    public EmployeeForCreationDto? ProjectManager { get; init; }
    public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
}
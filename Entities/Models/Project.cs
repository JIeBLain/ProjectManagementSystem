using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Project
{
    [Column("ProjectId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Project name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The name of the customer's company is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the CustomerCompany is 60 characters.")]
    public string? CustomerCompany { get; set; }

    [Required(ErrorMessage = "The name of the contractor's company is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the ContractorCompany is 60 characters.")]
    public string? ContractorCompany { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    [Required(ErrorMessage = "Priority is a required field.")]
    public int Priority { get; set; }
    public ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
}
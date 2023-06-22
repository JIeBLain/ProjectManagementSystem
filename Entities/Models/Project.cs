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
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    [Required(ErrorMessage = "Priority is a required field.")]
    public int Priority { get; set; }

    public int? ProjectManagerId { get; set; }
    public Employee? ProjectManager { get; set; }

    [ForeignKey("CustomerCompany")]
    public int? CustomerCompanyId { get; set; }
    public Company? CustomerCompany { get; set; }

    [ForeignKey("ContractorCompany")]
    public int? ContractorCompanyId { get; set; }
    public Company? ContractorCompany { get; set; }

    public ICollection<Employee>? Employees { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Company
{
    [Column("CompanyId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Company name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; set; }

    [InverseProperty("CustomerCompany")]
    public ICollection<Project>? CustomerProjects { get; set; }
    [InverseProperty("ContractorCompany")]
    public ICollection<Project>? ContractorProjects { get; set; }
}
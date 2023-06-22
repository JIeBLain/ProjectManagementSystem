using Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Employee
{
    [Column("EmployeeId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Employee first name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the FirstName is 30 characters.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Employee last name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the LastName is 30 characters.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Employee patronymic name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the PatronymicName is 30 characters.")]
    public string? PatronymicName { get; set; }

    [Required(ErrorMessage = "BirthDate is a required field.")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Gender is a required field.")]
    public Gender Gender { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? Phone { get; set; }
    public ICollection<Project>? Projects { get; set; }
}
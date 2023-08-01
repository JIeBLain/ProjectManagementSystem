using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record EmployeeForCreationDto
{

    [Required(ErrorMessage = "Employee first name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the FirstName is 30 characters.")]
    public string? FirstName { get; init; }

    [Required(ErrorMessage = "Employee last name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the LastName is 30 characters.")]
    public string? LastName { get; init; }

    [Required(ErrorMessage = "Employee patronymic name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the PatronymicName is 30 characters.")]
    public string? PatronymicName { get; init; }

    [Required(ErrorMessage = "BirthDate is a required field.")]
    public DateTime BirthDate { get; init; }

    [Required(ErrorMessage = "Gender is a required field.")]
    public string? Gender { get; init; }

    [EmailAddress]
    public string? Email { get; init; }

    [Phone]
    public string? Phone { get; init; }
}
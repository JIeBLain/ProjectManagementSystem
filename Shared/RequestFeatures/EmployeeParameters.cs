using Entities.Enums;

namespace Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public string? Gender { get; set; }

    public bool ValidGender
    {
        get
        {
            if (string.IsNullOrEmpty(Gender))
                return true;

            var gender = Enum.TryParse<Gender>(Gender, true, out var result)
                ? result
                : default;

            return Enum.IsDefined(typeof(Gender), gender);
        }
    }
}
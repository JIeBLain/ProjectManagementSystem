namespace Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public EmployeeParameters()
    {
        OrderBy = "lastName";
    }

    public string? Gender { get; set; }

    public bool ValidGender
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Gender))
                return true;

            return Gender.Equals("male", StringComparison.InvariantCultureIgnoreCase)
                || Gender.Equals("female", StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public string? SearchTerm { get; set; }
}
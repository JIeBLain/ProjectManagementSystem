using Entities.Enums;

namespace Repository.SupportingFunctionality;

public static class GenderParser
{
    public static Gender ConvertStringToGender(string gender)
    {
        return Enum.TryParse<Gender>(gender, true, out var result)
            ? result
            : default;
    }
}
using Entities.Enums;
using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, string gender)
    {
        var convertedToGender = ConvertStringToGender(gender);

        return employees
            .Where(e => string.IsNullOrWhiteSpace(gender) || e.Gender.Equals(convertedToGender));
    }

    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return employees;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return employees.Where(e => e.LastName.ToLower().Contains(lowerCaseTerm));
    }

    private static Gender ConvertStringToGender(string gender)
    {
        return Enum.TryParse<Gender>(gender, true, out var result)
            ? result
            : default;
    }
}
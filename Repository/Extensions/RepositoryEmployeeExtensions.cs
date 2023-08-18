using Entities.Enums;
using Entities.Models;
using Repository.Extensions.LuceneSearchLibrary;

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

        var searchEngine = new LuceneSearchEngine<Employee>(employees);
        var documents = searchEngine.Search(searchTerm, 1000);

        var searchedEmployees = new List<Employee>();
        foreach (var doc in documents)
        {
            searchedEmployees.Add(new Employee
            {
                Id = new Guid(doc.Get("Id")),
                FirstName = doc.Get("FirstName"),
                LastName = doc.Get("LastName"),
                PatronymicName = doc.Get("PatronymicName"),
                BirthDate = DateTime.Parse(doc.Get("BirthDate")),
                Gender = Enum.Parse<Gender>(doc.Get("Gender")),
                Email = doc.Get("Email"),
                Phone = doc.Get("Phone")
            });
        }

        return employees.Where(employee => searchedEmployees.Contains(employee));
    }

    private static Gender ConvertStringToGender(string gender)
    {
        return Enum.TryParse<Gender>(gender, true, out var result)
            ? result
            : default;
    }
}
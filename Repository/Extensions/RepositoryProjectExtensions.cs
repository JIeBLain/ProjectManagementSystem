using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryProjectExtensions
{
    public static IQueryable<Project> FilterProjects(this IQueryable<Project> projects, int minPriority, int maxPriority)
    {
        return projects
            .Where(p => p.Priority >= minPriority && p.Priority <= maxPriority);
    }

    public static IQueryable<Project> Search(this IQueryable<Project> projects, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return projects;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return projects.Where(p => p.Name.StartsWith(lowerCaseTerm) || p.Name.EndsWith(lowerCaseTerm));
    }
}
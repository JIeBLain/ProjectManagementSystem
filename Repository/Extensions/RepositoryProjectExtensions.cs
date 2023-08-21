using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

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

    public static IQueryable<Project> Sort(this IQueryable<Project> projects, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return projects.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Project>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return projects.OrderBy(e => e.Name);

        return projects.OrderBy(orderQuery);
    }
}
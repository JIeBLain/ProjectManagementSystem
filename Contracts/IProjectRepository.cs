using Entities.Models;

namespace Contracts;

public interface IProjectRepository
{
    IEnumerable<Project> GetAllProjects(bool trackChanges);
}
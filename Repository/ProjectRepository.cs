using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public ProjectRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Project> GetAllProjects(bool trackChanges)
    {
        return FindAll(trackChanges)
            .OrderBy(p => p.Name)
            .ToList();
    }

    public Project GetProject(Guid projectId, bool trackChanges)
    {
        return FindByCondition(p => p.Id.Equals(projectId), trackChanges)
            .SingleOrDefault();
    }

    public Project GetProjectByEmployee(Guid employeeId, Guid projectId, bool trackChanges)
    {
        return FindByCondition(p => p.ProjectEmployees.Any(pe => pe.EmployeeId.Equals(employeeId)), trackChanges)
            .SingleOrDefault(p => p.Id.Equals(projectId));
    }

    public IEnumerable<Project> GetProjectsByEmployee(Guid employeeId, bool trackChanges)
    {
        return FindByCondition(p => p.ProjectEmployees.Any(pe => pe.EmployeeId.Equals(employeeId)), trackChanges)
            .OrderBy(p => p.Name)
            .ToList();
    }

    public void CreateProject(Project project)
    {
        Create(project);
    }

    public void CreateProjectForEmployee(Guid employeeId, Project project)
    {
        var employee = RepositoryContext.Employees
            .SingleOrDefault(e => e.Id.Equals(employeeId));

        var projectEmployee = new ProjectEmployee { Project = project, Employee = employee };

        RepositoryContext.Add(projectEmployee);
        Create(project);
    }

    public IEnumerable<Project> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        return FindByCondition(p => ids.Contains(p.Id), trackChanges)
            .ToList();
    }

    public void DeleteProject(Project project)
    {
        Delete(project);
    }

    public void DeleteProjectForEmployee(Guid employeeId, Guid projectId, bool trackChanges)
    {
        var employee = RepositoryContext.Employees
            .Include(e => e.ProjectEmployees)
            .Include(e => e.ProjectManagerProjects)
            .SingleOrDefault(e => e.Id.Equals(employeeId));

        if (employee is not null)
        {
            var project = employee.ProjectManagerProjects
                .SingleOrDefault(p => p.Id.Equals(projectId));

            if (project is not null)
            {
                employee.ProjectManagerProjects.Remove(project);
            }
        }

        var projectEmployee = employee.ProjectEmployees
            .SingleOrDefault(pe => pe.ProjectId.Equals(projectId));

        RepositoryContext.Remove(projectEmployee);
    }
}
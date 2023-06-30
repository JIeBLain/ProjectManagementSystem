using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Project>? Projects { get; set; }
    public DbSet<Employee>? Employees { get; set; }
    public DbSet<ProjectEmployee>? ProjectEmployees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenderConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectEmployeeConfiguration());
    }
}
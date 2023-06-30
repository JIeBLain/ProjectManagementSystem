using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ProjectEmployeeConfiguration : IEntityTypeConfiguration<ProjectEmployees>
{
    public void Configure(EntityTypeBuilder<ProjectEmployees> builder)
    {
        builder.HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

        builder.HasOne(pe => pe.Employee)
            .WithMany(e => e.ProjectEmployees)
            .HasForeignKey(pe => pe.EmployeeId);

        builder.HasOne(pe => pe.Project)
            .WithMany(p => p.ProjectEmployees)
            .HasForeignKey(pe => pe.ProjectId);
    }
}
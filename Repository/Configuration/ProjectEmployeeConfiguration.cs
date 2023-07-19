using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ProjectEmployeeConfiguration : IEntityTypeConfiguration<ProjectEmployee>
{
    public void Configure(EntityTypeBuilder<ProjectEmployee> builder)
    {
        builder.HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

        builder.HasOne(pe => pe.Employee)
            .WithMany(e => e.ProjectEmployees)
            .HasForeignKey(pe => pe.EmployeeId);

        builder.HasOne(pe => pe.Project)
            .WithMany(p => p.ProjectEmployees)
            .HasForeignKey(pe => pe.ProjectId);

        builder.HasData(
             new ProjectEmployee
             {
                 ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                 EmployeeId = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"),
                 ProjectManagerId = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2")
             },
            new ProjectEmployee
            {
                ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                EmployeeId = new Guid("fadc9f8c-7023-444e-bf23-d788bc0b0f7b"),
                ProjectManagerId = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                EmployeeId = new Guid("89c34d1e-979b-4f9c-a41b-2db9cc0172d8"),
                ProjectManagerId = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                EmployeeId = new Guid("b6357c6d-171d-4382-9f3e-7db7760c55db"),
                ProjectManagerId = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                EmployeeId = new Guid("5c2dfb33-d692-4690-92cd-a37ae08144ec"),
                ProjectManagerId = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                EmployeeId = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"),
                ProjectManagerId = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                EmployeeId = new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"),
                ProjectManagerId = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                EmployeeId = new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"),
                ProjectManagerId = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                EmployeeId = new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"),
                ProjectManagerId = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                EmployeeId = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"),
                ProjectManagerId = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                EmployeeId = new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"),
                ProjectManagerId = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                EmployeeId = new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"),
                ProjectManagerId = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa")
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                EmployeeId = new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"),
                ProjectManagerId = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa")
            });
    }
}
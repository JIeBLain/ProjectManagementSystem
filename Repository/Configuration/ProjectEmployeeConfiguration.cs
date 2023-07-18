using Entities.Enums;
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

        var projectManagerForBigtax = new Employee
        {
            Id = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"),
            FirstName = "Vera",
            LastName = "Andryushchenko",
            PatronymicName = "Nikiforovna",
            BirthDate = new DateTime(1978, 09, 22),
            Gender = Gender.Female,
            Email = "vera1978@outlook.com",
            Phone = "+7 (942) 209-64-18"
        };

        var projectManagerForCookley = new Employee
        {
            Id = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"),
            FirstName = "Ilya",
            LastName = "Yukhtrits",
            PatronymicName = "Gerasimovich",
            BirthDate = new DateTime(1980, 09, 21),
            Gender = Gender.Male,
            Email = "ilya.yuhtric@outlook.com",
            Phone = "+7 (966) 747-44-68"
        };

        var projectManagerForZoolab = new Employee
        {
            Id = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"),
            FirstName = "Evgeny",
            LastName = "Bessonov",
            PatronymicName = "Vasilievich",
            BirthDate = new DateTime(1981, 09, 02),
            Gender = Gender.Male,
            Email = "evgeniy52@hotmail.com",
            Phone = "+7 (988) 418-40-86"
        };

        builder.HasData(
             new ProjectEmployee
             {
                 ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                 EmployeeId = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"),
                 ProjectManager = projectManagerForBigtax
             },
            new ProjectEmployee
            {
                ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                EmployeeId = new Guid("fadc9f8c-7023-444e-bf23-d788bc0b0f7b"),
                ProjectManager = projectManagerForBigtax
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                EmployeeId = new Guid("89c34d1e-979b-4f9c-a41b-2db9cc0172d8"),
                ProjectManager = projectManagerForBigtax
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                EmployeeId = new Guid("b6357c6d-171d-4382-9f3e-7db7760c55db"),
                ProjectManager = projectManagerForBigtax
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                EmployeeId = new Guid("5c2dfb33-d692-4690-92cd-a37ae08144ec"),
                ProjectManager = projectManagerForBigtax
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                EmployeeId = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"),
                ProjectManager = projectManagerForCookley
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                EmployeeId = new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"),
                ProjectManager = projectManagerForCookley
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                EmployeeId = new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"),
                ProjectManager = projectManagerForCookley
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                EmployeeId = new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"),
                ProjectManager = projectManagerForCookley
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                EmployeeId = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"),
                ProjectManager = projectManagerForZoolab
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                EmployeeId = new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"),
                ProjectManager = projectManagerForZoolab
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                EmployeeId = new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"),
                ProjectManager = projectManagerForZoolab
            },
            new ProjectEmployee
            {
                ProjectId = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                EmployeeId = new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"),
                ProjectManager = projectManagerForZoolab
            });
    }
}
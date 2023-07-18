using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasData(
            new Project
            {
                Id = new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                Name = "Zoolab",
                CustomerCompany = "Tagcat",
                ContractorCompany = "DevShare",
                StartTime = new DateTime(2023, 06, 18),
                EndTime = new DateTime(2024, 01, 24),
                Priority = 1
            },
            new Project
            {
                Id = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                Name = "Bigtax",
                CustomerCompany = "Roombo",
                ContractorCompany = "DevShare",
                StartTime = new DateTime(2023, 06, 19),
                EndTime = new DateTime(2023, 09, 07),
                Priority = 3
            },
            new Project
            {
                Id = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                Name = "Cookley",
                CustomerCompany = "Zoomdog",
                ContractorCompany = "DevShare",
                StartTime = new DateTime(2023, 05, 25),
                EndTime = new DateTime(2023, 10, 22),
                Priority = 2
            });
    }
}
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
                CustomerCompany = "",
                ContractorCompany = "DevShare",
                StartTime = new DateTime(2023, 06, 18),
                EndTime = new DateTime(2024, 01, 24),
                Priority = 1,
                ProjectManagerId = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa")
            },
            new Project
            {
                Id = new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                Name = "Bigtax",
                CustomerCompany = "",
                ContractorCompany = "DevShare",
                StartTime = new DateTime(2023, 06, 19),
                EndTime = new DateTime(2023, 09, 07),
                Priority = 3,
                ProjectManagerId = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2")
            },
            new Project
            {
                Id = new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                Name = "Cookley",
                CustomerCompany = "",
                ContractorCompany = "DevShare",
                StartTime = new DateTime(2023, 05, 25),
                EndTime = new DateTime(2023, 10, 22),
                Priority = 2,
                ProjectManagerId = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4")
            });
    }
}
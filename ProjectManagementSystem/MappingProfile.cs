using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace ProjectManagementSystem;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Project, ProjectDto>();

        CreateMap<Employee, EmployeeDto>()
            .ForCtorParam("FullName",
            opt => opt.MapFrom(e => string.Join(' ', e.LastName, e.FirstName, e.PatronymicName)));

        CreateMap<ProjectForCreationDto, Project>();

        CreateMap<EmployeeForCreationDto, Employee>();
    }
}
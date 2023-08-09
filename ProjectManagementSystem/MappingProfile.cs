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
            opt => opt.MapFrom(e => string.Join(' ', e.LastName, e.FirstName, e.PatronymicName)))
            .ForCtorParam("Age", opt => opt.MapFrom(e => CalculateAge(e.BirthDate)));

        CreateMap<ProjectEmployee, ProjectEmployeeDto>();

        CreateMap<ProjectForCreationDto, Project>();

        CreateMap<EmployeeForCreationDto, Employee>();

        CreateMap<ProjectEmployeeForCreationDto, ProjectEmployee>();

        CreateMap<ProjectForUpdateDto, Project>().ReverseMap();

        CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
    }

    private static int CalculateAge(DateTime birthdate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthdate.Year;

        if (birthdate.Date > today.AddYears(-age))
            age--;

        return age;
    }
}
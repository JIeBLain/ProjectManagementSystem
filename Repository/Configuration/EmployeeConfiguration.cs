using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(
            new Employee
            {
                Id = new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"),
                FirstName = "Vera",
                LastName = "Andryushchenko",
                PatronymicName = "Nikiforovna",
                BirthDate = new DateTime(1978, 09, 22),
                Gender = Gender.Female,
                Email = "vera1978@outlook.com",
                Phone = "+7 (942) 209-64-18"
            },
            new Employee
            {
                Id = new Guid("fadc9f8c-7023-444e-bf23-d788bc0b0f7b"),
                FirstName = "Taras",
                LastName = "Krivov",
                PatronymicName = "Daniilovich",
                BirthDate = new DateTime(1990, 03, 13),
                Gender = Gender.Male,
                Email = "taras13031990@hotmail.com",
                Phone = "+7 (936) 505-76-15"
            },
            new Employee
            {
                Id = new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"),
                FirstName = "Stepan",
                LastName = "Engelgardt",
                PatronymicName = "Nikanorovich",
                BirthDate = new DateTime(1993, 12, 16),
                Gender = Gender.Male,
                Email = "stepan1993@ya.ru",
                Phone = "+7 (945) 389-82-95"
            },
            new Employee
            {
                Id = new Guid("89c34d1e-979b-4f9c-a41b-2db9cc0172d8"),
                FirstName = "Varvara",
                LastName = "Korsakova",
                PatronymicName = "Konstantinovna",
                BirthDate = new DateTime(1986, 07, 23),
                Gender = Gender.Female,
                Email = "varvara34@yandex.ru",
                Phone = "+7 (920) 287-68-41"
            },
            new Employee
            {
                Id = new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"),
                FirstName = "Evgeny",
                LastName = "Bessonov",
                PatronymicName = "Vasilievich",
                BirthDate = new DateTime(1981, 09, 02),
                Gender = Gender.Male,
                Email = "evgeniy52@hotmail.com",
                Phone = "+7 (988) 418-40-86"
            }, new Employee
            {
                Id = new Guid("b6357c6d-171d-4382-9f3e-7db7760c55db"),
                FirstName = "Roman",
                LastName = "Ivashev",
                PatronymicName = "Alekseevich",
                BirthDate = new DateTime(1996, 01, 08),
                Gender = Gender.Male,
                Email = "roman1996@hotmail.com",
                Phone = "+7 (986) 656-79-26"
            },
            new Employee
            {
                Id = new Guid("5c2dfb33-d692-4690-92cd-a37ae08144ec"),
                FirstName = "Semyon",
                LastName = "Bibikov",
                PatronymicName = "Afanasyevich",
                BirthDate = new DateTime(1980, 01, 13),
                Gender = Gender.Male,
                Email = "semen8022@outlook.com",
                Phone = "+7 (913) 989-58-31"
            },
            new Employee
            {
                Id = new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"),
                FirstName = "Maria",
                LastName = "Kondratieva",
                PatronymicName = "Yurievna",
                BirthDate = new DateTime(1992, 10, 17),
                Gender = Gender.Female,
                Email = "mariya89@rambler.ru",
                Phone = "+7 (960) 807-19-78"
            },
            new Employee
            {
                Id = new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"),
                FirstName = "Ilya",
                LastName = "Yukhtrits",
                PatronymicName = "Gerasimovich",
                BirthDate = new DateTime(1980, 09, 21),
                Gender = Gender.Male,
                Email = "ilya.yuhtric@outlook.com",
                Phone = "+7 (966) 747-44-68"
            },
            new Employee
            {
                Id = new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"),
                FirstName = "Pavel",
                LastName = "Khoroshilov",
                PatronymicName = "Yakovlevich",
                BirthDate = new DateTime(1988, 11, 20),
                Gender = Gender.Male,
                Email = "pavel2404@outlook.com",
                Phone = "+7 (953) 115-54-54"
            });
    }
}
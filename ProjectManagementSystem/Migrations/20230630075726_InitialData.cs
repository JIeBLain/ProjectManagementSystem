using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "BirthDate", "Email", "FirstName", "Gender", "LastName", "PatronymicName", "Phone" },
                values: new object[,]
                {
                    { new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"), new DateTime(1993, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "stepan1993@ya.ru", "Stepan", "Male", "Engelgardt", "Nikanorovich", "+7 (945) 389-82-95" },
                    { new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"), new DateTime(1981, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "evgeniy52@hotmail.com", "Evgeny", "Male", "Bessonov", "Vasilievich", "+7 (988) 418-40-86" },
                    { new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"), new DateTime(1992, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariya89@rambler.ru", "Maria", "Female", "Kondratieva", "Yurievna", "+7 (960) 807-19-78" },
                    { new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"), new DateTime(1980, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "ilya.yuhtric@outlook.com", "Ilya", "Male", "Yukhtrits", "Gerasimovich", "+7 (966) 747-44-68" },
                    { new Guid("5c2dfb33-d692-4690-92cd-a37ae08144ec"), new DateTime(1980, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "semen8022@outlook.com", "Semyon", "Male", "Bibikov", "Afanasyevich", "+7 (913) 989-58-31" },
                    { new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"), new DateTime(1978, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "vera1978@outlook.com", "Vera", "Female", "Andryushchenko", "Nikiforovna", "+7 (942) 209-64-18" },
                    { new Guid("89c34d1e-979b-4f9c-a41b-2db9cc0172d8"), new DateTime(1986, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "varvara34@yandex.ru", "Varvara", "Female", "Korsakova", "Konstantinovna", "+7 (920) 287-68-41" },
                    { new Guid("b6357c6d-171d-4382-9f3e-7db7760c55db"), new DateTime(1996, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "roman1996@hotmail.com", "Roman", "Male", "Ivashev", "Alekseevich", "+7 (986) 656-79-26" },
                    { new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"), new DateTime(1988, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "pavel2404@outlook.com", "Pavel", "Male", "Khoroshilov", "Yakovlevich", "+7 (953) 115-54-54" },
                    { new Guid("fadc9f8c-7023-444e-bf23-d788bc0b0f7b"), new DateTime(1990, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "taras13031990@hotmail.com", "Taras", "Male", "Krivov", "Daniilovich", "+7 (936) 505-76-15" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "ContractorCompany", "CustomerCompany", "EndTime", "Name", "Priority", "ProjectManagerId", "StartTime" },
                values: new object[,]
                {
                    { new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"), "DevShare", "", new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cookley", 2, new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"), new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"), "DevShare", "", new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bigtax", 3, new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"), new DateTime(2023, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"), "DevShare", "", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zoolab", 1, new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"), new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ProjectEmployees",
                columns: new[] { "EmployeeId", "ProjectId" },
                values: new object[,]
                {
                    { new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") },
                    { new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") },
                    { new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") },
                    { new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") },
                    { new Guid("5c2dfb33-d692-4690-92cd-a37ae08144ec"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                    { new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                    { new Guid("89c34d1e-979b-4f9c-a41b-2db9cc0172d8"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                    { new Guid("b6357c6d-171d-4382-9f3e-7db7760c55db"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                    { new Guid("fadc9f8c-7023-444e-bf23-d788bc0b0f7b"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                    { new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") },
                    { new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") },
                    { new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") },
                    { new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("5c2dfb33-d692-4690-92cd-a37ae08144ec"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("89c34d1e-979b-4f9c-a41b-2db9cc0172d8"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("b6357c6d-171d-4382-9f3e-7db7760c55db"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("fadc9f8c-7023-444e-bf23-d788bc0b0f7b"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("5c2dfb33-d692-4690-92cd-a37ae08144ec"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("89c34d1e-979b-4f9c-a41b-2db9cc0172d8"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("b6357c6d-171d-4382-9f3e-7db7760c55db"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("fadc9f8c-7023-444e-bf23-d788bc0b0f7b"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"));
        }
    }
}

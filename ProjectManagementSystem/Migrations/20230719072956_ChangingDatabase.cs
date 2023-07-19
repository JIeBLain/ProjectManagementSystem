using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectManagerId",
                table: "ProjectEmployees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") },
                column: "ProjectManagerId",
                value: new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") },
                column: "ProjectManagerId",
                value: new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") },
                column: "ProjectManagerId",
                value: new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"), new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335") },
                column: "ProjectManagerId",
                value: new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("5c2dfb33-d692-4690-92cd-a37ae08144ec"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                column: "ProjectManagerId",
                value: new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                column: "ProjectManagerId",
                value: new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("89c34d1e-979b-4f9c-a41b-2db9cc0172d8"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                column: "ProjectManagerId",
                value: new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("b6357c6d-171d-4382-9f3e-7db7760c55db"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                column: "ProjectManagerId",
                value: new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("fadc9f8c-7023-444e-bf23-d788bc0b0f7b"), new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e") },
                column: "ProjectManagerId",
                value: new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("17a5124d-ef58-4cc7-a2ad-91f4ee0677ed"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") },
                column: "ProjectManagerId",
                value: new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") },
                column: "ProjectManagerId",
                value: new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("47ea38ca-233c-4f43-af5c-c69dbc1c002a"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") },
                column: "ProjectManagerId",
                value: new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"));

            migrationBuilder.UpdateData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("c92fbb01-10f1-4f54-894a-0a3a560f2d63"), new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2") },
                column: "ProjectManagerId",
                value: new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa"));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                column: "CustomerCompany",
                value: "Zoomdog");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                column: "CustomerCompany",
                value: "Roombo");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                column: "CustomerCompany",
                value: "Tagcat");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployees_ProjectManagerId",
                table: "ProjectEmployees",
                column: "ProjectManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_Employees_ProjectManagerId",
                table: "ProjectEmployees",
                column: "ProjectManagerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_Employees_ProjectManagerId",
                table: "ProjectEmployees");

            migrationBuilder.DropIndex(
                name: "IX_ProjectEmployees_ProjectManagerId",
                table: "ProjectEmployees");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "ProjectEmployees");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("41c1b4fe-63f2-4c8e-a02f-76027ccf7335"),
                columns: new[] { "CustomerCompany", "ProjectManagerId" },
                values: new object[] { "", new Guid("4ebe2333-57aa-421a-9ea8-fc8fdb9838a4") });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("a99d6d3c-9269-41da-a967-64dddd97ca3e"),
                columns: new[] { "CustomerCompany", "ProjectManagerId" },
                values: new object[] { "", new Guid("774ef45f-9896-41fc-ac08-b16e6b3cc2a2") });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("f9de832e-4732-442f-b7fd-ef4c035a99e2"),
                columns: new[] { "CustomerCompany", "ProjectManagerId" },
                values: new object[] { "", new Guid("2cd2000a-b580-4fce-be7e-07dbbe6685fa") });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}

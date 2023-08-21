using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class IndexingFullNameForEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastName_FirstName_PatronymicName",
                table: "Employees",
                columns: new[] { "LastName", "FirstName", "PatronymicName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_LastName_FirstName_PatronymicName",
                table: "Employees");
        }
    }
}

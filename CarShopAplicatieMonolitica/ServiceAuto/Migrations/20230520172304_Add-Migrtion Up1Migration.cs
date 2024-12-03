using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrtionUp1Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeServiceName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeServiceName",
                table: "Employees");
        }
    }
}

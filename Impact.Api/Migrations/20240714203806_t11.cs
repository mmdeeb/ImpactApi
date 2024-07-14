using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class t11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "employeeAccounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "employeeAccounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

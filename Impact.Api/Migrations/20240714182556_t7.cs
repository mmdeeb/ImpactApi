using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class t7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResEmployee",
                table: "restaurants",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "restaurants",
                newName: "ResEmployee");
        }
    }
}

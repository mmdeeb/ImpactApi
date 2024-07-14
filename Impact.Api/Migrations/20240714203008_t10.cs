using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class t10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdsLink",
                table: "ads",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdsLink",
                table: "ads");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class t5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mails_restaurantAccounts_RestaurantAccountId",
                table: "mails");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantAccountId",
                table: "mails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_mails_restaurantAccounts_RestaurantAccountId",
                table: "mails",
                column: "RestaurantAccountId",
                principalTable: "restaurantAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mails_restaurantAccounts_RestaurantAccountId",
                table: "mails");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantAccountId",
                table: "mails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_mails_restaurantAccounts_RestaurantAccountId",
                table: "mails",
                column: "RestaurantAccountId",
                principalTable: "restaurantAccounts",
                principalColumn: "Id");
        }
    }
}

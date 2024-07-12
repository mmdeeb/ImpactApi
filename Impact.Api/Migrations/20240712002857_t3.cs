using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class t3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_trainings_TrainingId",
                table: "attendances");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingId",
                table: "attendances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_trainings_TrainingId",
                table: "attendances",
                column: "TrainingId",
                principalTable: "trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_trainings_TrainingId",
                table: "attendances");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingId",
                table: "attendances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_trainings_TrainingId",
                table: "attendances",
                column: "TrainingId",
                principalTable: "trainings",
                principalColumn: "Id");
        }
    }
}

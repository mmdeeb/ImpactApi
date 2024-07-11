using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class t2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_additionalCosts_trainingInvoices_TrainingInvoiceId",
                table: "additionalCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_additionalCosts_trainings_TrainingId",
                table: "additionalCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_logisticCosts_trainingInvoices_TrainingInvoiceId",
                table: "logisticCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_mails_trainingInvoices_TrainingInvoiceId",
                table: "mails");

            migrationBuilder.DropForeignKey(
                name: "FK_mails_trainings_TrainingId",
                table: "mails");

            migrationBuilder.DropIndex(
                name: "IX_mails_TrainingId",
                table: "mails");

            migrationBuilder.DropIndex(
                name: "IX_logisticCosts_TrainingInvoiceId",
                table: "logisticCosts");

            migrationBuilder.DropIndex(
                name: "IX_additionalCosts_TrainingId",
                table: "additionalCosts");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "mails");

            migrationBuilder.DropColumn(
                name: "TrainingInvoiceId",
                table: "logisticCosts");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "additionalCosts");

            migrationBuilder.DropColumn(
                name: "TraningId",
                table: "additionalCosts");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingInvoiceId",
                table: "mails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TrainingInvoiceId",
                table: "additionalCosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_additionalCosts_trainingInvoices_TrainingInvoiceId",
                table: "additionalCosts",
                column: "TrainingInvoiceId",
                principalTable: "trainingInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mails_trainingInvoices_TrainingInvoiceId",
                table: "mails",
                column: "TrainingInvoiceId",
                principalTable: "trainingInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_additionalCosts_trainingInvoices_TrainingInvoiceId",
                table: "additionalCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_mails_trainingInvoices_TrainingInvoiceId",
                table: "mails");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingInvoiceId",
                table: "mails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                table: "mails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainingInvoiceId",
                table: "logisticCosts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TrainingInvoiceId",
                table: "additionalCosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                table: "additionalCosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TraningId",
                table: "additionalCosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_mails_TrainingId",
                table: "mails",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_logisticCosts_TrainingInvoiceId",
                table: "logisticCosts",
                column: "TrainingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_additionalCosts_TrainingId",
                table: "additionalCosts",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_additionalCosts_trainingInvoices_TrainingInvoiceId",
                table: "additionalCosts",
                column: "TrainingInvoiceId",
                principalTable: "trainingInvoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_additionalCosts_trainings_TrainingId",
                table: "additionalCosts",
                column: "TrainingId",
                principalTable: "trainings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_logisticCosts_trainingInvoices_TrainingInvoiceId",
                table: "logisticCosts",
                column: "TrainingInvoiceId",
                principalTable: "trainingInvoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_mails_trainingInvoices_TrainingInvoiceId",
                table: "mails",
                column: "TrainingInvoiceId",
                principalTable: "trainingInvoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_mails_trainings_TrainingId",
                table: "mails",
                column: "TrainingId",
                principalTable: "trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

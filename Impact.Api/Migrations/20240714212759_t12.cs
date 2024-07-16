using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class t12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_otherExpenses_users_EmployeeId",
                table: "otherExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_receipts_logisticCosts_LogisticCostId",
                table: "receipts");

            migrationBuilder.DropTable(
                name: "financialFunds");

            migrationBuilder.DropTable(
                name: "logisticCosts");

            migrationBuilder.DropIndex(
                name: "IX_receipts_LogisticCostId",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "LogisticCostId",
                table: "receipts");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "otherExpenses",
                newName: "CenterId");

            migrationBuilder.RenameIndex(
                name: "IX_otherExpenses_EmployeeId",
                table: "otherExpenses",
                newName: "IX_otherExpenses_CenterId");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "otherExpenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_otherExpenses_centers_CenterId",
                table: "otherExpenses",
                column: "CenterId",
                principalTable: "centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_otherExpenses_centers_CenterId",
                table: "otherExpenses");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "otherExpenses");

            migrationBuilder.RenameColumn(
                name: "CenterId",
                table: "otherExpenses",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_otherExpenses_CenterId",
                table: "otherExpenses",
                newName: "IX_otherExpenses_EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "LogisticCostId",
                table: "receipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "financialFunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DebtOnTheFund = table.Column<double>(type: "float", nullable: false),
                    DebtToTheFund = table.Column<double>(type: "float", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financialFunds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "logisticCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoInvoiceURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalBalance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logisticCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_logisticCosts_centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_receipts_LogisticCostId",
                table: "receipts",
                column: "LogisticCostId");

            migrationBuilder.CreateIndex(
                name: "IX_logisticCosts_CenterId",
                table: "logisticCosts",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_otherExpenses_users_EmployeeId",
                table: "otherExpenses",
                column: "EmployeeId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_receipts_logisticCosts_LogisticCostId",
                table: "receipts",
                column: "LogisticCostId",
                principalTable: "logisticCosts",
                principalColumn: "Id");
        }
    }
}

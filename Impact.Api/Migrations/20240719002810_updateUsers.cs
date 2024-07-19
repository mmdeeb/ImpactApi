using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class updateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainings_users_ClientId",
                table: "trainings");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_trainings_ClientId",
                table: "trainings");

            migrationBuilder.AddColumn<string>(
                name: "ClintId",
                table: "trainings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientAccountId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeAccountId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_trainings_ClintId",
                table: "trainings",
                column: "ClintId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CenterId",
                table: "AspNetUsers",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientAccountId",
                table: "AspNetUsers",
                column: "ClientAccountId",
                unique: true,
                filter: "[ClientAccountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeAccountId",
                table: "AspNetUsers",
                column: "EmployeeAccountId",
                unique: true,
                filter: "[EmployeeAccountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_centers_CenterId",
                table: "AspNetUsers",
                column: "CenterId",
                principalTable: "centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_clientAccounts_ClientAccountId",
                table: "AspNetUsers",
                column: "ClientAccountId",
                principalTable: "clientAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_employeeAccounts_EmployeeAccountId",
                table: "AspNetUsers",
                column: "EmployeeAccountId",
                principalTable: "employeeAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trainings_AspNetUsers_ClintId",
                table: "trainings",
                column: "ClintId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_centers_CenterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_clientAccounts_ClientAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_employeeAccounts_EmployeeAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_trainings_AspNetUsers_ClintId",
                table: "trainings");

            migrationBuilder.DropIndex(
                name: "IX_trainings_ClintId",
                table: "trainings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CenterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClientAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClintId",
                table: "trainings");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAccountId = table.Column<int>(type: "int", nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: true),
                    EmployeeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_clientAccounts_ClientAccountId",
                        column: x => x.ClientAccountId,
                        principalTable: "clientAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_users_employeeAccounts_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "employeeAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trainings_ClientId",
                table: "trainings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_users_CenterId",
                table: "users",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_users_ClientAccountId",
                table: "users",
                column: "ClientAccountId",
                unique: true,
                filter: "[ClientAccountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_EmployeeAccountId",
                table: "users",
                column: "EmployeeAccountId",
                unique: true,
                filter: "[EmployeeAccountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_trainings_users_ClientId",
                table: "trainings",
                column: "ClientId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

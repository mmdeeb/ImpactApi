using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impact.Api.Migrations
{
    /// <inheritdoc />
    public partial class t1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aboutUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Links = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListAdsMedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdsTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdsDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "centers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Media = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_centers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clientAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    TotalBalance = table.Column<double>(type: "float", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employeeAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Deduct = table.Column<double>(type: "float", nullable: true),
                    AdvancePayment = table.Column<double>(type: "float", nullable: true),
                    Reward = table.Column<double>(type: "float", nullable: true),
                    TotalBalance = table.Column<double>(type: "float", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "financialFunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebtOnTheFund = table.Column<double>(type: "float", nullable: false),
                    DebtToTheFund = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financialFunds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "restaurantAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalBalance = table.Column<double>(type: "float", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurantAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainerSpecialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trainingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "halls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    ListDetials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_halls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_halls_centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainingInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealsCost = table.Column<double>(type: "float", nullable: false),
                    TrainerCost = table.Column<double>(type: "float", nullable: false),
                    PhotoInvoiceURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservationsCost = table.Column<double>(type: "float", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    FinalCost = table.Column<double>(type: "float", nullable: false),
                    ClientAccountId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainingInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trainingInvoices_clientAccounts_ClientAccountId",
                        column: x => x.ClientAccountId,
                        principalTable: "clientAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ClientAccountId = table.Column<int>(type: "int", nullable: true),
                    EmployeeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResEmployee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantAccountId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_restaurants_restaurantAccounts_RestaurantAccountId",
                        column: x => x.RestaurantAccountId,
                        principalTable: "restaurantAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTrainingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTrainingDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingTypeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subTrainings_trainingTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "trainingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "logisticCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoInvoiceURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    TotalBalance = table.Column<double>(type: "float", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingInvoiceId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_logisticCosts_trainingInvoices_TrainingInvoiceId",
                        column: x => x.TrainingInvoiceId,
                        principalTable: "trainingInvoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "otherExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoInvoiceURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_otherExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_otherExpenses_users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfStudents = table.Column<int>(type: "int", nullable: false),
                    TrainingDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingInvoiceId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trainings_trainingInvoices_TrainingInvoiceId",
                        column: x => x.TrainingInvoiceId,
                        principalTable: "trainingInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trainings_users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubTrainingTrainer",
                columns: table => new
                {
                    SubTrainingId = table.Column<int>(type: "int", nullable: false),
                    TrainersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTrainingTrainer", x => new { x.SubTrainingId, x.TrainersId });
                    table.ForeignKey(
                        name: "FK_SubTrainingTrainer_subTrainings_SubTrainingId",
                        column: x => x.SubTrainingId,
                        principalTable: "subTrainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubTrainingTrainer_trainers_TrainersId",
                        column: x => x.TrainersId,
                        principalTable: "trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    LogisticCostId = table.Column<int>(type: "int", nullable: true),
                    ClientAccountId = table.Column<int>(type: "int", nullable: true),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: true),
                    RestaurantAccountId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_receipts_clientAccounts_ClientAccountId",
                        column: x => x.ClientAccountId,
                        principalTable: "clientAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_receipts_employeeAccounts_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "employeeAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_receipts_logisticCosts_LogisticCostId",
                        column: x => x.LogisticCostId,
                        principalTable: "logisticCosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_receipts_restaurantAccounts_RestaurantAccountId",
                        column: x => x.RestaurantAccountId,
                        principalTable: "restaurantAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "additionalCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Detailes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoInvoiceURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingId = table.Column<int>(type: "int", nullable: true),
                    TraningId = table.Column<int>(type: "int", nullable: false),
                    TrainingInvoiceId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additionalCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_additionalCosts_trainingInvoices_TrainingInvoiceId",
                        column: x => x.TrainingInvoiceId,
                        principalTable: "trainingInvoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_additionalCosts_trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendanceName = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendances_trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailPrice = table.Column<double>(type: "float", nullable: false),
                    MailPriceForORG = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    TotalPriceForORG = table.Column<double>(type: "float", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    RestaurantAccountId = table.Column<int>(type: "int", nullable: true),
                    TrainingInvoiceId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mails_restaurantAccounts_RestaurantAccountId",
                        column: x => x.RestaurantAccountId,
                        principalTable: "restaurantAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mails_trainingInvoices_TrainingInvoiceId",
                        column: x => x.TrainingInvoiceId,
                        principalTable: "trainingInvoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mails_trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservations_halls_HallId",
                        column: x => x.HallId,
                        principalTable: "halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "trainees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListAttendanceStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    AttendanceId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trainees_attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "attendances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_trainees_trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_additionalCosts_TrainingId",
                table: "additionalCosts",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_additionalCosts_TrainingInvoiceId",
                table: "additionalCosts",
                column: "TrainingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_attendances_TrainingId",
                table: "attendances",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_halls_CenterId",
                table: "halls",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_logisticCosts_CenterId",
                table: "logisticCosts",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_logisticCosts_TrainingInvoiceId",
                table: "logisticCosts",
                column: "TrainingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_mails_RestaurantAccountId",
                table: "mails",
                column: "RestaurantAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_mails_TrainingId",
                table: "mails",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_mails_TrainingInvoiceId",
                table: "mails",
                column: "TrainingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_otherExpenses_EmployeeId",
                table: "otherExpenses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_ClientAccountId",
                table: "receipts",
                column: "ClientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_EmployeeAccountId",
                table: "receipts",
                column: "EmployeeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_LogisticCostId",
                table: "receipts",
                column: "LogisticCostId");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_RestaurantAccountId",
                table: "receipts",
                column: "RestaurantAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_HallId",
                table: "reservations",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_TrainingId",
                table: "reservations",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_RestaurantAccountId",
                table: "restaurants",
                column: "RestaurantAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subTrainings_TrainingTypeId",
                table: "subTrainings",
                column: "TrainingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTrainingTrainer_TrainersId",
                table: "SubTrainingTrainer",
                column: "TrainersId");

            migrationBuilder.CreateIndex(
                name: "IX_trainees_AttendanceId",
                table: "trainees",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_trainees_TrainingId",
                table: "trainees",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_trainingInvoices_ClientAccountId",
                table: "trainingInvoices",
                column: "ClientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_trainings_ClientId",
                table: "trainings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_trainings_TrainingInvoiceId",
                table: "trainings",
                column: "TrainingInvoiceId",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aboutUs");

            migrationBuilder.DropTable(
                name: "additionalCosts");

            migrationBuilder.DropTable(
                name: "ads");

            migrationBuilder.DropTable(
                name: "financialFunds");

            migrationBuilder.DropTable(
                name: "mails");

            migrationBuilder.DropTable(
                name: "otherExpenses");

            migrationBuilder.DropTable(
                name: "receipts");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "restaurants");

            migrationBuilder.DropTable(
                name: "SubTrainingTrainer");

            migrationBuilder.DropTable(
                name: "trainees");

            migrationBuilder.DropTable(
                name: "logisticCosts");

            migrationBuilder.DropTable(
                name: "halls");

            migrationBuilder.DropTable(
                name: "restaurantAccounts");

            migrationBuilder.DropTable(
                name: "subTrainings");

            migrationBuilder.DropTable(
                name: "trainers");

            migrationBuilder.DropTable(
                name: "attendances");

            migrationBuilder.DropTable(
                name: "trainingTypes");

            migrationBuilder.DropTable(
                name: "trainings");

            migrationBuilder.DropTable(
                name: "trainingInvoices");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "centers");

            migrationBuilder.DropTable(
                name: "clientAccounts");

            migrationBuilder.DropTable(
                name: "employeeAccounts");
        }
    }
}

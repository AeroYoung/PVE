using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PVE.Migrations
{
    public partial class iNIT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PveData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNum = table.Column<string>(nullable: false),
                    Producer = table.Column<string>(nullable: true),
                    VehicleType = table.Column<string>(nullable: true),
                    OBD = table.Column<string>(nullable: true),
                    BOB = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: true),
                    VehicleNum = table.Column<int>(nullable: true),
                    VIN = table.Column<string>(nullable: false),
                    TestContent = table.Column<string>(nullable: true),
                    ProgressJ1 = table.Column<string>(nullable: true),
                    ProgressJ2D = table.Column<string>(nullable: true),
                    ProgressJ2Z = table.Column<string>(nullable: true),
                    ProgressJ2W = table.Column<string>(nullable: true),
                    ProgressJ2H = table.Column<string>(nullable: true),
                    ProgressJ2S = table.Column<string>(nullable: true),
                    ProgressJ3 = table.Column<string>(nullable: true),
                    ContactCustomer = table.Column<string>(nullable: true),
                    ContactMarket = table.Column<string>(nullable: true),
                    ContactCATAC = table.Column<string>(nullable: true),
                    Period = table.Column<string>(nullable: true),
                    ContractType = table.Column<string>(nullable: true),
                    Agreement = table.Column<string>(nullable: true),
                    ProjectBid = table.Column<double>(nullable: true),
                    FeeJ1 = table.Column<double>(nullable: true),
                    FeeJ2 = table.Column<double>(nullable: true),
                    FeeJ3 = table.Column<double>(nullable: true),
                    TaskForm = table.Column<string>(nullable: true),
                    ReportDate = table.Column<DateTime>(nullable: true),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    FeeStatus = table.Column<string>(nullable: true),
                    ProjectStatus = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PveData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCode",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PveDataID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCode", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ErrorCode_PveData_PveDataID",
                        column: x => x.PveDataID,
                        principalTable: "PveData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "J2TestData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PveDataID = table.Column<int>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    TestGroup = table.Column<string>(nullable: true),
                    ActualTestDate = table.Column<DateTime>(nullable: false),
                    SoftVersion = table.Column<string>(nullable: true),
                    SABVersion = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_J2TestData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_J2TestData_PveData_PveDataID",
                        column: x => x.PveDataID,
                        principalTable: "PveData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PveTestData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PveDataID = table.Column<int>(nullable: false),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    BeginMan = table.Column<string>(nullable: true),
                    BeginCompany = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Problem = table.Column<string>(nullable: true),
                    Priors = table.Column<string>(nullable: true),
                    ChargeMan = table.Column<string>(nullable: true),
                    ChargeCompany = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    FeedPercent = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    L19011 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PveTestData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PveTestData_PveData_PveDataID",
                        column: x => x.PveDataID,
                        principalTable: "PveData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Signal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PveDataID = table.Column<int>(nullable: false),
                    PinNo = table.Column<string>(nullable: false),
                    PinName = table.Column<string>(nullable: true),
                    Func1 = table.Column<string>(nullable: true),
                    Func2 = table.Column<string>(nullable: true),
                    OBD = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Signal_PveData_PveDataID",
                        column: x => x.PveDataID,
                        principalTable: "PveData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCode_PveDataID",
                table: "ErrorCode",
                column: "PveDataID");

            migrationBuilder.CreateIndex(
                name: "IX_J2TestData_PveDataID",
                table: "J2TestData",
                column: "PveDataID");

            migrationBuilder.CreateIndex(
                name: "IX_PveData_VIN",
                table: "PveData",
                column: "VIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PveTestData_PveDataID",
                table: "PveTestData",
                column: "PveDataID");

            migrationBuilder.CreateIndex(
                name: "IX_Signal_PveDataID",
                table: "Signal",
                column: "PveDataID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorCode");

            migrationBuilder.DropTable(
                name: "J2TestData");

            migrationBuilder.DropTable(
                name: "PveTestData");

            migrationBuilder.DropTable(
                name: "Signal");

            migrationBuilder.DropTable(
                name: "PveData");
        }
    }
}

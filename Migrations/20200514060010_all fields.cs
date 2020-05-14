using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PVE.Migrations
{
    public partial class allfields : Migration
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
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    VehicleNum = table.Column<int>(nullable: false),
                    VIN = table.Column<string>(nullable: true),
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
                    ProjectBid = table.Column<double>(nullable: false),
                    FeeJ1 = table.Column<double>(nullable: false),
                    FeeJ2 = table.Column<double>(nullable: false),
                    FeeJ3 = table.Column<double>(nullable: false),
                    TaskForm = table.Column<string>(nullable: true),
                    ReportDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    FeeStatus = table.Column<string>(nullable: true),
                    ProjectStatus = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PveData", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PveData");
        }
    }
}

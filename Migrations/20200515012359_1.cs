using Microsoft.EntityFrameworkCore.Migrations;

namespace PVE.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signal_PveData_PveDataID",
                table: "Signal");

            migrationBuilder.AlterColumn<int>(
                name: "PveDataID",
                table: "Signal",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Signal_PveData_PveDataID",
                table: "Signal",
                column: "PveDataID",
                principalTable: "PveData",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signal_PveData_PveDataID",
                table: "Signal");

            migrationBuilder.AlterColumn<int>(
                name: "PveDataID",
                table: "Signal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Signal_PveData_PveDataID",
                table: "Signal",
                column: "PveDataID",
                principalTable: "PveData",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

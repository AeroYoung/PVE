using Microsoft.EntityFrameworkCore.Migrations;

namespace PVE.Migrations
{
    public partial class SignalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "PveData",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Signal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PveDataID = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PveData_VIN",
                table: "PveData",
                column: "VIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Signal_PveDataID",
                table: "Signal",
                column: "PveDataID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Signal");

            migrationBuilder.DropIndex(
                name: "IX_PveData_VIN",
                table: "PveData");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "PveData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

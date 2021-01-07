using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesManagementSystem.Migrations
{
    public partial class FixSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_EmployeeId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_EmployeeId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Schedules");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Schedules",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeId",
                table: "Schedules",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AspNetUsers_EmployeeId",
                table: "Schedules",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_EmployeeId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_EmployeeId",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "Schedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeId1",
                table: "Schedules",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AspNetUsers_EmployeeId1",
                table: "Schedules",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

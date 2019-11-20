using Microsoft.EntityFrameworkCore.Migrations;

namespace STD_Course_Management.Migrations
{
    public partial class AppuserUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Dept_DeptId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DeptId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DeptId",
                table: "AspNetUsers",
                newName: "DeptName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeptName",
                table: "AspNetUsers",
                newName: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DeptId",
                table: "AspNetUsers",
                column: "DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Dept_DeptId",
                table: "AspNetUsers",
                column: "DeptId",
                principalTable: "Dept",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

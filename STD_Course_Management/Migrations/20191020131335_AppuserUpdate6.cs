using Microsoft.EntityFrameworkCore.Migrations;

namespace STD_Course_Management.Migrations
{
    public partial class AppuserUpdate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Student_id",
                table: "CourseConfirm",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseConfirm_Student_id",
                table: "CourseConfirm",
                column: "Student_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseConfirm_AspNetUsers_Student_id",
                table: "CourseConfirm",
                column: "Student_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseConfirm_AspNetUsers_Student_id",
                table: "CourseConfirm");

            migrationBuilder.DropIndex(
                name: "IX_CourseConfirm_Student_id",
                table: "CourseConfirm");

            migrationBuilder.DropColumn(
                name: "Student_id",
                table: "CourseConfirm");
        }
    }
}

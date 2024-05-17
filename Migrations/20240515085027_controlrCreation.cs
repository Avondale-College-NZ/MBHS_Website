using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MBHS_Website.Migrations
{
    /// <inheritdoc />
    public partial class controlrCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_AspNetUsers_TeacherId",
                table: "SubjectTeacher");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "SubjectTeacher",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_AspNetUsers_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_AspNetUsers_TeacherId",
                table: "SubjectTeacher");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "SubjectTeacher",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_AspNetUsers_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

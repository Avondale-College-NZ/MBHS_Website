using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MBHS_Website.Migrations
{
    /// <inheritdoc />
    public partial class departmentss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Subject");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_DepartmentId",
                table: "Subject",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Department_DepartmentId",
                table: "Subject",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Department_DepartmentId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Subject_DepartmentId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

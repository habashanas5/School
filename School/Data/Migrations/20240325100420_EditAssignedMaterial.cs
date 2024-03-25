using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditAssignedMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedMaterial_Teacher_TeacherID",
                table: "AssignedMaterial");

            migrationBuilder.DropIndex(
                name: "IX_AssignedMaterial_TeacherID",
                table: "AssignedMaterial");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "AssignedMaterial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "AssignedMaterial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedMaterial_TeacherID",
                table: "AssignedMaterial",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedMaterial_Teacher_TeacherID",
                table: "AssignedMaterial",
                column: "TeacherID",
                principalTable: "Teacher",
                principalColumn: "TeacherID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

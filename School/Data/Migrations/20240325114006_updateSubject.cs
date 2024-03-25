using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectName",
                table: "Subject",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SubjectID",
                table: "Subject",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subject",
                newName: "SubjectName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subject",
                newName: "SubjectID");
        }
    }
}

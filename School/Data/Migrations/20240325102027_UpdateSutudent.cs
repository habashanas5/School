using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSutudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentPhoneNo",
                table: "Student",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "StudentName",
                table: "Student",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StudentEmail",
                table: "Student",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Student",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Student",
                newName: "StudentPhoneNo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Student",
                newName: "StudentName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Student",
                newName: "StudentEmail");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Student",
                newName: "StudentID");
        }
    }
}

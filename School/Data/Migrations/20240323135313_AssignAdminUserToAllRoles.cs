using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignAdminUserToAllRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       //     migrationBuilder.Sql("INSERT INTO [security].[UserRoles](UserId,RoleId) SELECT 'ac86e89a-8af2-4287-bf3b-4dbab9217249', Id FROM [security].[Roles]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
     //       migrationBuilder.Sql("DELETE FROM [security].[UserRoles] WHERE UserId = 'ac86e89a-8af2-4287-bf3b-4dbab9217249'");
        }
    }
}

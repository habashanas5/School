using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Policy;

#nullable disable

namespace School.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      //      migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'ac86e89a-8af2-4287-bf3b-4dbab9217249', N'habashanas716@gmail.com', N'HABASHANAS716@GMAIL.COM', N'habashanas716@gmail.com', N'HABASHANAS716@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEKoy0mMNA2OX9FE+OFTqdUtnMh+rxrMhBMls+NUzFadLvYohWhlJXHQKu9eNXTf67g==', N'HF3IU7JPEMKKBWOWH6DROGTOHY3H5GDC', N'd4a58453-1a4d-40e1-ae84-e377f8537443', 0592834293, 0, 0, NULL, 1, 0, N'Anas', N'Al-Habash', NULL)");
        }

        /// <inheritdoc />  
        protected override void Down(MigrationBuilder migrationBuilder)
        {
      //      migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id = 'ac86e89a-8af2-4287-bf3b-4dbab9217249'");
        }
    }
}

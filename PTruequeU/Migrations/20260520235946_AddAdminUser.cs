using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTruequeU.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsSuspended", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProgramName", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aaaaaaaa-aaaa-aaaa-aaaa-000000000000", 0, "CONCSTAMP-STATIC-000000000000000", "admin@email.com", true, null, false, false, null, "ADMIN@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENx4zxiRrAfPtkZV5vFfJ+RgEHgHrAvRjf6bdYMLzInq/XWu7gibD/0GBKoPO3EBEg==", null, false, null, 0.0, "SECSTAMP-STATIC-0000000000000000", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", "aaaaaaaa-aaaa-aaaa-aaaa-000000000000" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", "aaaaaaaa-aaaa-aaaa-aaaa-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000000");
        }
    }
}

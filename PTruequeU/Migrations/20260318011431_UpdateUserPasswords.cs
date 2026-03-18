using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTruequeU.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000001",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENx4zxiRrAfPtkZV5vFfJ+RgEHgHrAvRjf6bdYMLzInq/XWu7gibD/0GBKoPO3EBEg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000002",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENx4zxiRrAfPtkZV5vFfJ+RgEHgHrAvRjf6bdYMLzInq/XWu7gibD/0GBKoPO3EBEg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000003",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENx4zxiRrAfPtkZV5vFfJ+RgEHgHrAvRjf6bdYMLzInq/XWu7gibD/0GBKoPO3EBEg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000001",
                column: "PasswordHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000002",
                column: "PasswordHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000003",
                column: "PasswordHash",
                value: null);
        }
    }
}

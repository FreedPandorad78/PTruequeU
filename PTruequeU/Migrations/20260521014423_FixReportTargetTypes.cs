using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTruequeU.Migrations
{
    /// <inheritdoc />
    public partial class FixReportTargetTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000002"),
                column: "TargetType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000005"),
                column: "TargetType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000009"),
                column: "TargetType",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000002"),
                column: "TargetType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000005"),
                column: "TargetType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000009"),
                column: "TargetType",
                value: 0);
        }
    }
}

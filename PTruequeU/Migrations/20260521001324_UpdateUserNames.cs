using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTruequeU.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000000",
                columns: new[] { "FullName", "ProgramName" },
                values: new object[] { "Administrador", "Administración" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000001",
                columns: new[] { "FullName", "ProgramName" },
                values: new object[] { "Andrea García", "Ingeniería de Sistemas" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000002",
                columns: new[] { "FullName", "ProgramName" },
                values: new object[] { "Bruno Martínez", "Administración de Empresas" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000003",
                columns: new[] { "FullName", "ProgramName" },
                values: new object[] { "Carla López", "Diseño Industrial" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000000",
                columns: new[] { "FullName", "ProgramName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000001",
                columns: new[] { "FullName", "ProgramName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000002",
                columns: new[] { "FullName", "ProgramName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000003",
                columns: new[] { "FullName", "ProgramName" },
                values: new object[] { null, null });
        }
    }
}

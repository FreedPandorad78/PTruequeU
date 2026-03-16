using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTruequeU.Migrations
{
    /// <inheritdoc />
    public partial class AddReportsModerationAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModerationActions",
                columns: table => new
                {
                    ModerationAction_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Admin_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModerationActions", x => x.ModerationAction_Id);
                    table.ForeignKey(
                        name: "FK_ModerationActions_AspNetUsers_Admin_Id",
                        column: x => x.Admin_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Report_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reporter_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportedUser_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReportedListing_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetType = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Report_Id);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_ReportedUser_Id",
                        column: x => x.ReportedUser_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_Reporter_Id",
                        column: x => x.Reporter_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Listings_ReportedListing_Id",
                        column: x => x.ReportedListing_Id,
                        principalTable: "Listings",
                        principalColumn: "Listing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModerationActions_Admin_Id",
                table: "ModerationActions",
                column: "Admin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ModerationActions_CreatedAt",
                table: "ModerationActions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ModerationActions_TargetId",
                table: "ModerationActions",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CreatedAt",
                table: "Reports",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportedListing_Id",
                table: "Reports",
                column: "ReportedListing_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportedUser_Id",
                table: "Reports",
                column: "ReportedUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Reporter_Id",
                table: "Reports",
                column: "Reporter_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModerationActions");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}

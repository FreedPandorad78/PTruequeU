using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTruequeU.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoritesModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatThreads",
                columns: table => new
                {
                    ChatThread_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listing_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Buyer_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Seller_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatThreads", x => x.ChatThread_Id);
                    table.ForeignKey(
                        name: "FK_ChatThreads_AspNetUsers_Buyer_Id",
                        column: x => x.Buyer_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatThreads_AspNetUsers_Seller_Id",
                        column: x => x.Seller_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatThreads_Listings_Listing_Id",
                        column: x => x.Listing_Id,
                        principalTable: "Listings",
                        principalColumn: "Listing_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Favorite_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listing_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Favorite_Id);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Listings_Listing_Id",
                        column: x => x.Listing_Id,
                        principalTable: "Listings",
                        principalColumn: "Listing_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    ChatMessage_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Thread_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sender_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.ChatMessage_Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_Sender_Id",
                        column: x => x.Sender_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatThreads_Thread_Id",
                        column: x => x.Thread_Id,
                        principalTable: "ChatThreads",
                        principalColumn: "ChatThread_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_Sender_Id",
                table: "ChatMessages",
                column: "Sender_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_Thread_Id",
                table: "ChatMessages",
                column: "Thread_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatThreads_Buyer_Id",
                table: "ChatThreads",
                column: "Buyer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatThreads_Listing_Id_Buyer_Id_Seller_Id",
                table: "ChatThreads",
                columns: new[] { "Listing_Id", "Buyer_Id", "Seller_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatThreads_Seller_Id",
                table: "ChatThreads",
                column: "Seller_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_Listing_Id",
                table: "Favorites",
                column: "Listing_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_User_Id_Listing_Id",
                table: "Favorites",
                columns: new[] { "User_Id", "Listing_Id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "ChatThreads");
        }
    }
}

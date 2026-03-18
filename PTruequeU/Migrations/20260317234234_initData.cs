using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PTruequeU.Migrations
{
    /// <inheritdoc />
    public partial class initData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsSuspended", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProgramName", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", 0, "CONCSTAMP-STATIC-000000000000001", "andrea@email.com", true, null, false, false, null, "ANDREA@EMAIL.COM", "ANDREA", null, null, false, null, 0.0, "SECSTAMP-STATIC-0000000000000001", false, "andrea" },
                    { "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", 0, "CONCSTAMP-STATIC-000000000000002", "bruno@email.com", true, null, false, false, null, "BRUNO@EMAIL.COM", "BRUNO", null, null, false, null, 0.0, "SECSTAMP-STATIC-0000000000000002", false, "bruno" },
                    { "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", 0, "CONCSTAMP-STATIC-000000000000003", "carla@email.com", true, null, false, false, null, "CARLA@EMAIL.COM", "CARLA", null, null, false, null, 0.0, "SECSTAMP-STATIC-0000000000000003", false, "carla" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_Id", "Name" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), "Electrónica" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"), "Hogar" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"), "Ropa" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"), "Deportes" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000005"), "Libros" }
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "Listing_id", "Category_Id", "Condition", "CreatedAt", "Description", "IsHidden", "Location", "Price", "State", "Title", "UpdatedAt", "User_Id" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000001"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), 0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Excelente estado, cargador original incluido.", false, "", 120000m, 0, "Celular Samsung Galaxy A54", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000002"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), 0, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Intel i5, 8GB RAM, SSD 256GB, sin golpes.", false, "", 350000m, 0, "Laptop HP Pavilion 15", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000003"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), 0, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Cancelación de ruido activa, poco uso.", false, "", 95000m, 0, "Audífonos Sony WH-1000XM4", new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000004"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), 0, new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Pantalla 10 pulgadas, 64GB, con funda protectora.", false, "", 85000m, 0, "Tablet Lenovo Tab M10", new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000005"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"), 0, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Ergonómica, reposapiés incluido, color negro/rojo.", false, "", 80000m, 0, "Silla gamer RGB", new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000006"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"), 0, new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Tela beige, sin manchas, desmontable.", false, "", 180000m, 0, "Sofá de 3 puestos", new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000007"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"), 0, new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Uso doméstico, incluye jarra de leche.", false, "", 45000m, 0, "Cafetera espresso Oster", new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000008"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"), 0, new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Talla M, muy poco uso, estilo biker.", false, "", 60000m, 0, "Chaqueta de cuero negra", new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000009"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"), 0, new DateTime(2023, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Talla 32x32, corte slim, azul oscuro.", false, "", 35000m, 0, "Jeans Levi's 511", new DateTime(2023, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000010"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"), 0, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Talla 42, usadas 3 veces, blanco/gris.", false, "", 70000m, 0, "Zapatillas Nike Air Max 90", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000011"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"), 0, new DateTime(2023, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Rodada 29, frenos hidráulicos, 21 velocidades.", false, "", 250000m, 0, "Bicicleta de montaña Trek", new DateTime(2023, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000012"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"), 0, new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Par completo con barra y discos, recubrimiento goma.", false, "", 55000m, 0, "Mancuernas ajustables 20kg", new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000013"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"), 0, new DateTime(2023, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Head 100, grip 4, en funda original.", false, "", 40000m, 0, "Raqueta de tenis Wilson", new DateTime(2023, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000014"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000005"), 0, new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Edición en español, marcas de lápiz leves.", false, "", 25000m, 0, "Libro Clean Code", new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("cccccccc-cccc-cccc-cccc-000000000015"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000005"), 0, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "4ta edición, como nuevo, sin subrayados.", false, "", 30000m, 0, "Libro C# en Profundidad", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Report_Id", "Comment", "CreatedAt", "Reason", "ReportedListing_Id", "ReportedUser_Id", "Reporter_Id", "TargetType" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-000000000002"), "Me pidió hacer la transferencia fuera de la plataforma.", new DateTime(2023, 1, 14, 11, 30, 0, 0, DateTimeKind.Utc), "Intento de estafa", null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", 0 },
                    { new Guid("11111111-1111-1111-1111-000000000005"), "Envió mensajes ofensivos durante la negociación del precio.", new DateTime(2023, 1, 20, 8, 45, 0, 0, DateTimeKind.Utc), "Comportamiento inapropiado", null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", 0 },
                    { new Guid("11111111-1111-1111-1111-000000000009"), "El perfil parece ser un bot, respuestas automáticas y rápidas.", new DateTime(2023, 1, 29, 9, 30, 0, 0, DateTimeKind.Utc), "Cuenta sospechosa", null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", 0 }
                });

            migrationBuilder.InsertData(
                table: "ChatThreads",
                columns: new[] { "ChatThread_Id", "Buyer_Id", "CreatedAt", "Listing_Id", "Seller_Id" },
                values: new object[,]
                {
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000001"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000001"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000002"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000001"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000003"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000002"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000004"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000002"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000005"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000003"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000006"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000004"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000007"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000005"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000008"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000005"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000009"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000006"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000010"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000006"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000011"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000007"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000012"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000008"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000013"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000008"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000014"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000009"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000015"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000010"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000016"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000011"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000017"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000011"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000018"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000012"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000019"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000014"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-000000000020"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-000000000015"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001" }
                });

            migrationBuilder.InsertData(
                table: "ListingImages",
                columns: new[] { "ListingImage_Id", "DisplayOrder", "ImageUrl", "Listing_Id" },
                values: new object[,]
                {
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000001"), 0, "img/samsung-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000001") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000002"), 0, "img/samsung-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000001") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000003"), 0, "img/samsung-3.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000001") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000004"), 0, "img/laptop-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000002") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000005"), 0, "img/laptop-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000002") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000006"), 0, "img/laptop-3.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000002") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000007"), 0, "img/audifonos-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000003") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000008"), 0, "img/audifonos-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000003") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000009"), 0, "img/tablet-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000004") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000010"), 0, "img/tablet-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000004") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000011"), 0, "img/silla-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000005") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000012"), 0, "img/silla-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000005") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000013"), 0, "img/sofa-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000006") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000014"), 0, "img/sofa-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000006") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000015"), 0, "img/cafetera-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000007") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000016"), 0, "img/cafetera-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000007") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000017"), 0, "img/chaqueta-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000008") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000018"), 0, "img/chaqueta-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000008") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000019"), 0, "img/jeans-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000009") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000020"), 0, "img/jeans-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000009") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000021"), 0, "img/nike-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000010") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000022"), 0, "img/nike-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000010") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000023"), 0, "img/bici-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000011") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000024"), 0, "img/bici-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000011") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000025"), 0, "img/bici-3.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000011") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000026"), 0, "img/mancuernas-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000012") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000027"), 0, "img/mancuernas-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000012") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000028"), 0, "img/raqueta-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000013") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000029"), 0, "img/raqueta-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000013") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000030"), 0, "img/cleancode-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000014") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000031"), 0, "img/cleancode-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000014") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000032"), 0, "img/csharp-1.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000015") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000033"), 0, "img/csharp-2.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000015") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000034"), 0, "img/csharp-3.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000015") },
                    { new Guid("dddddddd-dddd-dddd-dddd-000000000035"), 0, "img/samsung-4.jpg", new Guid("cccccccc-cccc-cccc-cccc-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Report_Id", "Comment", "CreatedAt", "Reason", "ReportedListing_Id", "ReportedUser_Id", "Reporter_Id", "TargetType" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-000000000001"), "El precio es demasiado bajo para ese modelo, posible robo.", new DateTime(2023, 1, 12, 10, 0, 0, 0, DateTimeKind.Utc), "Precio sospechoso", new Guid("cccccccc-cccc-cccc-cccc-000000000001"), null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", 0 },
                    { new Guid("11111111-1111-1111-1111-000000000003"), "Hay 3 publicaciones idénticas de este usuario.", new DateTime(2023, 1, 16, 9, 0, 0, 0, DateTimeKind.Utc), "Publicación duplicada", new Guid("cccccccc-cccc-cccc-cccc-000000000005"), null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", 0 },
                    { new Guid("11111111-1111-1111-1111-000000000004"), "Las fotos no corresponden al producto que se entrega.", new DateTime(2023, 1, 18, 14, 0, 0, 0, DateTimeKind.Utc), "Descripción engañosa", new Guid("cccccccc-cccc-cccc-cccc-000000000002"), null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", 0 },
                    { new Guid("11111111-1111-1111-1111-000000000006"), "La chaqueta tiene logos de marca que parecen réplica.", new DateTime(2023, 1, 22, 16, 0, 0, 0, DateTimeKind.Utc), "Artículo falsificado", new Guid("cccccccc-cccc-cccc-cccc-000000000008"), null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", 0 },
                    { new Guid("11111111-1111-1111-1111-000000000007"), "El vendedor confirmó que fue vendido pero la publicación sigue.", new DateTime(2023, 1, 25, 10, 15, 0, 0, DateTimeKind.Utc), "Producto ya vendido", new Guid("cccccccc-cccc-cccc-cccc-000000000011"), null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", 0 },
                    { new Guid("11111111-1111-1111-1111-000000000008"), "Parece ser una copia no autorizada, no el original.", new DateTime(2023, 1, 27, 13, 0, 0, 0, DateTimeKind.Utc), "Derechos de autor", new Guid("cccccccc-cccc-cccc-cccc-000000000014"), null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", 0 },
                    { new Guid("11111111-1111-1111-1111-000000000010"), "El mismo libro nuevo cuesta menos en cualquier librería física.", new DateTime(2023, 2, 1, 17, 0, 0, 0, DateTimeKind.Utc), "Precio inflado", new Guid("cccccccc-cccc-cccc-cccc-000000000015"), null, "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", 0 }
                });

            migrationBuilder.InsertData(
                table: "ChatMessages",
                columns: new[] { "ChatMessage_Id", "Sender_Id", "SentAt", "Text", "Thread_Id" },
                values: new object[,]
                {
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000001"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), "Hola, ¿el celular sigue disponible?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000001") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000002"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 10, 10, 5, 0, 0, DateTimeKind.Utc), "Sí, está disponible. ¿Tienes alguna duda?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000001") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000003"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 10, 10, 8, 0, 0, DateTimeKind.Utc), "¿Aceptas algo menos de precio?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000001") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000004"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 11, 9, 0, 0, 0, DateTimeKind.Utc), "¿El Samsung tiene algún golpe o rallón?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000002") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000005"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 11, 9, 3, 0, 0, DateTimeKind.Utc), "Ninguno, está impecable.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000002") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000006"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 12, 14, 0, 0, 0, DateTimeKind.Utc), "¿Cuál es la autonomía de batería de la laptop?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000003") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000007"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 12, 14, 10, 0, 0, DateTimeKind.Utc), "Unas 5 horas de uso normal.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000003") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000008"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 13, 8, 30, 0, 0, DateTimeKind.Utc), "¿La laptop tiene Windows original instalado?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000004") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000009"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 13, 8, 35, 0, 0, DateTimeKind.Utc), "Sí, Windows 11 activado y sin problemas.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000004") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000010"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 14, 11, 0, 0, 0, DateTimeKind.Utc), "¿Los audífonos vienen con estuche original?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000005") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000011"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 14, 11, 6, 0, 0, DateTimeKind.Utc), "Sí, estuche, cable y adaptador incluidos.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000005") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000012"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 15, 16, 0, 0, 0, DateTimeKind.Utc), "¿La silla soporta más de 100kg?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000007") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000013"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 15, 16, 4, 0, 0, DateTimeKind.Utc), "Sí, hasta 130kg según el fabricante.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000007") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000014"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 16, 10, 0, 0, 0, DateTimeKind.Utc), "¿Puedes bajar un poco el precio de la silla?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000008") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000015"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 16, 10, 7, 0, 0, DateTimeKind.Utc), "Te la dejo en 75.000 si cerramos hoy.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000008") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000016"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 17, 13, 0, 0, 0, DateTimeKind.Utc), "¿El sofá se puede desmontar para transportarlo?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000009") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000017"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 17, 13, 5, 0, 0, DateTimeKind.Utc), "Sí, en partes cabe sin problema en un auto mediano.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000009") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000018"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 18, 9, 0, 0, 0, DateTimeKind.Utc), "¿Cuánto tiempo tiene de uso la cafetera?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000011") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000019"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 18, 9, 4, 0, 0, DateTimeKind.Utc), "La compré hace 4 meses, tiene muy poco uso.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000011") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000020"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 19, 10, 0, 0, 0, DateTimeKind.Utc), "¿La chaqueta es de cuero genuino o sintético?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000012") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000021"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 19, 10, 5, 0, 0, DateTimeKind.Utc), "Es cuero genuino, tengo la factura original.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000012") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000022"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", new DateTime(2023, 1, 20, 15, 0, 0, 0, DateTimeKind.Utc), "¿La bici tiene cambios Shimano?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000016") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000023"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 20, 15, 3, 0, 0, DateTimeKind.Utc), "Sí, Shimano Altus de 21 velocidades.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000016") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000024"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 21, 10, 0, 0, 0, DateTimeKind.Utc), "¿Hacen envío o solo retiro en persona?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000017") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000025"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 21, 10, 6, 0, 0, DateTimeKind.Utc), "Solo retiro, la bici es muy voluminosa.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000017") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000026"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 22, 8, 0, 0, 0, DateTimeKind.Utc), "¿Las mancuernas son de hierro fundido?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000018") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000027"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 22, 8, 5, 0, 0, DateTimeKind.Utc), "Sí, con recubrimiento de goma antideslizante.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000018") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000028"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", new DateTime(2023, 1, 23, 11, 0, 0, 0, DateTimeKind.Utc), "¿Clean Code trae ejercicios prácticos?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000019") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000029"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 23, 11, 8, 0, 0, DateTimeKind.Utc), "Tiene ejemplos de código reales en cada capítulo.", new Guid("eeeeeeee-eeee-eeee-eeee-000000000019") },
                    { new Guid("ffffffff-ffff-ffff-ffff-000000000030"), "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", new DateTime(2023, 1, 24, 14, 0, 0, 0, DateTimeKind.Utc), "¿El libro de C# cubre .NET 6 o superior?", new Guid("eeeeeeee-eeee-eeee-eeee-000000000020") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000001"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000002"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000003"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000004"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000005"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000006"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000007"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000008"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000009"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000010"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000011"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000012"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000013"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000014"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000015"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000016"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000017"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000018"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000019"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000020"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000021"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000022"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000023"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000024"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000025"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000026"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000027"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000028"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000029"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "ChatMessage_Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-000000000030"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000006"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000010"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000013"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000014"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000015"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000001"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000002"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000003"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000004"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000005"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000006"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000007"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000008"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000009"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000010"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000011"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000012"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000013"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000014"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000015"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000016"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000017"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000018"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000019"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000020"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000021"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000022"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000023"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000024"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000025"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000026"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000027"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000028"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000029"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000030"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000031"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000032"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000033"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000034"));

            migrationBuilder.DeleteData(
                table: "ListingImages",
                keyColumn: "ListingImage_Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-000000000035"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000001"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000002"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000003"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000004"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000005"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000006"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000007"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000008"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000009"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Report_Id",
                keyValue: new Guid("11111111-1111-1111-1111-000000000010"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000001"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000002"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000003"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000004"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000005"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000007"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000008"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000009"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000011"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000012"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000016"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000017"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000018"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000019"));

            migrationBuilder.DeleteData(
                table: "ChatThreads",
                keyColumn: "ChatThread_Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-000000000020"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000004"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000009"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000010"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000013"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000001"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000002"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000003"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000005"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000006"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000007"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000008"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000011"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000012"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000014"));

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Listing_id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-000000000015"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000001");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000002");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-000000000003");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000005"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_Id", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "Electrónica" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "Hogar" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), "Ropa" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), "Deportes" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"), "Libros" }
                });
        }
    }
}

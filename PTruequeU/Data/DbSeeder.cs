using Microsoft.AspNetCore.Identity;
using PTruequeU.Models;
using PTruequeU.Models.Enums;

namespace PTruequeU.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.EnsureCreatedAsync();

            // Seed Roles
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Users (if not already seeded)
            if (!context.Users.Any())
            {
                var users = new List<(string email, string fullName, string program, string role)>
                {
                    ("admin@truequeu.edu", "Admin TruequeU", "Administración", "Admin"),
                    ("carlos.martinez@truequeu.edu", "Carlos Martínez", "Ingeniería de Sistemas", "User"),
                    ("maria.lopez@truequeu.edu", "María López", "Ingeniería Web", "User"),
                    ("juan.garcia@truequeu.edu", "Juan García", "Diseño Gráfico", "User"),
                    ("ana.rodriguez@truequeu.edu", "Ana Rodríguez", "Ingeniería Industrial", "User"),
                    ("pedro.sanchez@truequeu.edu", "Pedro Sánchez", "Comunicación Social", "User"),
                };

                var createdUsers = new List<ApplicationUser>();
                foreach (var (email, fullName, program, role) in users)
                {
                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = fullName,
                        Program = program,
                        EmailConfirmed = true,
                        CreatedAt = DateTime.UtcNow
                    };
                    await userManager.CreateAsync(user, "Password123!");
                    await userManager.AddToRoleAsync(user, role);
                    createdUsers.Add(user);
                }

                // Seed Categories
                var categories = new List<Category>
                {
                    new() { Name = "Libros", Description = "Libros de texto y lectura" },
                    new() { Name = "Electrónica", Description = "Dispositivos electrónicos y accesorios" },
                    new() { Name = "Ropa", Description = "Ropa y accesorios de moda" },
                    new() { Name = "Muebles", Description = "Muebles y decoración para hogar" },
                    new() { Name = "Deportes", Description = "Artículos deportivos" },
                    new() { Name = "Instrumentos", Description = "Instrumentos musicales" },
                    new() { Name = "Material Escolar", Description = "Útiles escolares y de oficina" },
                    new() { Name = "Transporte", Description = "Bicicletas, patinetas, etc." }
                };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();

                // Seed 15 Listings
                var listingData = new List<(string title, string description, ListingCondition condition, decimal price, string location, int categoryIdx, int userIdx)>
                {
                    ("Cálculo de Stewart 8va Ed.", "Libro de cálculo en excelente estado, sin rayones", ListingCondition.LikeNew, 45000, "Campus Norte", 0, 1),
                    ("Laptop HP Pavilion 15", "Laptop usada con i5 11th gen, 8GB RAM, 256GB SSD", ListingCondition.Good, 1200000, "Campus Central", 1, 2),
                    ("Guitarra acústica Yamaha C40", "Guitarra clásica, incluye funda", ListingCondition.Good, 350000, "Campus Sur", 5, 3),
                    ("Escritorio plegable IKEA", "Escritorio compacto ideal para apartamento", ListingCondition.LikeNew, 180000, "Campus Norte", 3, 4),
                    ("Balón de fútbol Adidas", "Balón oficial talla 5, poco uso", ListingCondition.LikeNew, 80000, "Campus Central", 4, 1),
                    ("Camiseta universitaria XL", "Camiseta oficial de la universidad, talla XL", ListingCondition.New, 55000, "Campus Central", 2, 5),
                    ("Programación en C# - libro", "Libro de programación C# para principiantes", ListingCondition.Good, 35000, "Campus Norte", 0, 2),
                    ("Monitor Samsung 24 pulgadas", "Monitor Full HD, HDMI y VGA", ListingCondition.Good, 450000, "Campus Sur", 1, 3),
                    ("Mochila Totto escolar", "Mochila grande con compartimento para laptop", ListingCondition.LikeNew, 95000, "Campus Central", 6, 4),
                    ("Bicicleta GW montaña", "Bicicleta rin 26, 21 velocidades", ListingCondition.Fair, 520000, "Campus Norte", 7, 1),
                    ("Teclado mecánico Redragon", "Teclado gaming RGB, switches blue", ListingCondition.LikeNew, 150000, "Campus Central", 1, 5),
                    ("Set de acuarelas Winsor", "Set profesional de 24 colores", ListingCondition.New, 120000, "Campus Sur", 6, 2),
                    ("Silla ergonómica", "Silla de escritorio con soporte lumbar", ListingCondition.Good, 280000, "Campus Norte", 3, 3),
                    ("Física de Serway Vol 1", "Libro de física universitaria, buena condición", ListingCondition.Fair, 30000, "Campus Central", 0, 4),
                    ("Audífonos Sony WH-1000XM4", "Audífonos noise cancelling, poco uso", ListingCondition.LikeNew, 680000, "Campus Sur", 1, 5),
                };

                var listings = new List<Listing>();
                foreach (var (title, description, condition, price, location, categoryIdx, userIdx) in listingData)
                {
                    var listing = new Listing
                    {
                        Title = title,
                        Description = description,
                        Condition = condition,
                        Price = price,
                        Location = location,
                        CategoryId = categories[categoryIdx].Id,
                        UserId = createdUsers[userIdx].Id,
                        State = ListingState.Available,
                        CreatedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 30)),
                        UpdatedAt = DateTime.UtcNow
                    };
                    listings.Add(listing);
                }
                context.Listings.AddRange(listings);
                await context.SaveChangesAsync();

                // Seed 35 Images: 3 per listing for the first 5, then 2 each for the remaining 10 → 15+20=35
                int[] imagesPerListing = { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
                for (int i = 0; i < listings.Count; i++)
                {
                    for (int j = 0; j < imagesPerListing[i]; j++)
                    {
                        context.ListingImages.Add(new ListingImage
                        {
                            ListingId = listings[i].Id,
                            ImageUrl = $"https://picsum.photos/seed/listing{listings[i].Id}img{j}/400/300",
                            DisplayOrder = j,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
                await context.SaveChangesAsync();

                // Seed 20 ChatRooms and 30 Messages
                var chatRooms = new List<ChatRoom>();
                var chatPairs = new List<(int listingIdx, int buyerIdx)>
                {
                    (0, 2), (0, 3), (1, 1), (1, 4), (2, 1),
                    (2, 2), (3, 5), (4, 3), (5, 1), (5, 4),
                    (6, 3), (7, 1), (7, 4), (8, 2), (9, 3),
                    (10, 4), (11, 1), (12, 5), (13, 2), (14, 3),
                };

                foreach (var (listingIdx, buyerIdx) in chatPairs)
                {
                    var listing = listings[listingIdx];
                    var buyer = createdUsers[buyerIdx];
                    if (buyer.Id == listing.UserId) continue; // Skip if buyer is seller

                    chatRooms.Add(new ChatRoom
                    {
                        ListingId = listing.Id,
                        BuyerId = buyer.Id,
                        SellerId = listing.UserId,
                        CreatedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 15))
                    });
                }
                context.ChatRooms.AddRange(chatRooms);
                await context.SaveChangesAsync();

                // Seed 30 Messages distributed across chat rooms
                var messageTexts = new[]
                {
                    "Hola, ¿aún está disponible?",
                    "Sí, todavía lo tengo",
                    "¿Cuál es el precio final?",
                    "El precio publicado es el final",
                    "¿Podemos vernos en campus central?",
                    "Claro, ¿a qué hora te queda bien?",
                    "Mañana a las 2pm",
                    "Perfecto, nos vemos ahí",
                    "¿Aceptas trueque?",
                    "Depende, ¿qué tienes para ofrecer?",
                    "¿Tiene algún defecto?",
                    "No, está en perfecto estado",
                    "Me interesa mucho",
                    "Genial, te lo puedo mostrar cuando quieras",
                    "¿Puedes bajar un poco el precio?",
                    "Puedo hacer un descuento pequeño",
                    "Listo, lo quiero",
                    "¿Te sirve transferencia?",
                    "Sí, acepto transferencia o efectivo",
                    "¿Cuánto tiempo de uso tiene?",
                    "Aproximadamente 6 meses",
                    "¿Incluye accesorios?",
                    "Sí, incluye todo lo original",
                    "Excelente, quedamos entonces",
                    "¿Podrías enviar más fotos?",
                    "Claro, te las envío por aquí",
                    "Gracias por la información",
                    "De nada, cualquier duda me escribes",
                    "¿Lo puedo ver antes de comprarlo?",
                    "Por supuesto, coordínamos"
                };

                var messageIdx = 0;
                foreach (var chatRoom in chatRooms)
                {
                    if (messageIdx >= 30) break;

                    // 1-2 messages per chat room
                    int messagesInRoom = (messageIdx < 20) ? 2 : 1;
                    if (messageIdx + messagesInRoom > 30) messagesInRoom = 30 - messageIdx;

                    for (int m = 0; m < messagesInRoom; m++)
                    {
                        var senderId = (m % 2 == 0) ? chatRoom.BuyerId : chatRoom.SellerId;
                        context.ChatMessages.Add(new ChatMessage
                        {
                            ChatRoomId = chatRoom.Id,
                            SenderId = senderId,
                            Content = messageTexts[messageIdx % messageTexts.Length],
                            SentAt = chatRoom.CreatedAt.AddMinutes(messageIdx * 10 + m * 5),
                            IsRead = m == 0
                        });
                        messageIdx++;
                    }
                }
                await context.SaveChangesAsync();

                // Seed 10 Reports
                var reportData = new List<(ReportTargetType type, int? listingIdx, int? reportedUserIdx, int reporterIdx, ReportReason reason, string comment)>
                {
                    (ReportTargetType.Listing, 1, null, 3, ReportReason.Scam, "El precio parece demasiado bajo para ser real"),
                    (ReportTargetType.Listing, 5, null, 4, ReportReason.Spam, "Publicación duplicada"),
                    (ReportTargetType.User, null, 5, 1, ReportReason.Offensive, "Lenguaje inapropiado en el chat"),
                    (ReportTargetType.Listing, 7, null, 2, ReportReason.Inappropriate, "Imagen no corresponde al producto"),
                    (ReportTargetType.Listing, 9, null, 5, ReportReason.Scam, "El producto no existe"),
                    (ReportTargetType.User, null, 3, 2, ReportReason.Other, "No se presentó a la cita acordada"),
                    (ReportTargetType.Listing, 11, null, 1, ReportReason.Spam, "Publicación repetida varias veces"),
                    (ReportTargetType.Listing, 3, null, 5, ReportReason.Inappropriate, "Descripción engañosa del producto"),
                    (ReportTargetType.User, null, 4, 3, ReportReason.Offensive, "Comentarios agresivos"),
                    (ReportTargetType.Listing, 14, null, 2, ReportReason.Scam, "Precio sospechosamente bajo"),
                };

                foreach (var (type, listingIdx, reportedUserIdx, reporterIdx, reason, comment) in reportData)
                {
                    context.Reports.Add(new Report
                    {
                        TargetType = type,
                        ListingId = listingIdx.HasValue ? listings[listingIdx.Value].Id : null,
                        ReportedUserId = reportedUserIdx.HasValue ? createdUsers[reportedUserIdx.Value].Id : null,
                        Reason = reason,
                        Comment = comment,
                        ReporterId = createdUsers[reporterIdx].Id,
                        CreatedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 10))
                    });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

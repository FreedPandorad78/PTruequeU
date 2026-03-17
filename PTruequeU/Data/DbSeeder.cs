using Microsoft.EntityFrameworkCore;
using PTruequeU.Models;

namespace PTruequeU.Data
{
    public static class DbSeeder
    {
        private static readonly string[] ListingTitles =
        {
            "iPhone 12 64GB en buen estado",
            "Bicicleta MTB aro 29",
            "Laptop Lenovo i5 8GB RAM",
            "Audífonos Sony inalámbricos",
            "Teclado mecánico RGB",
            "Monitor LG 24 pulgadas",
            "Cafetera Oster casi nueva",
            "Silla gamer ergonómica", 
            "Libro C# para principiantes",
            "Patineta semi profesional",
            "Mesa de estudio de madera",
            "Zapatillas deportivas talla 42",
            "Mochila universitaria impermeable",
            "Parlante JBL portátil",
            "Smartwatch Xiaomi"
        };

        private static readonly string[] ListingDescriptions =
        {
            "Producto funcional, con detalles mínimos de uso.",
            "Se entrega probado y en buen estado general.",
            "Ideal para estudiantes, uso diario sin problemas.",
            "Incluye accesorios básicos y caja genérica.",
            "Precio negociable, entrega en lugar público."
        };

        private static readonly string[] MessageSamples =
        {
            "Hola, ¿sigue disponible?",
            "¿Me compartes más fotos por favor?",
            "¿Cuál es el último precio?",
            "¿Haces entrega hoy?",
            "Me interesa, ¿podemos coordinar?"
        };

        private static readonly string[] ReportReasonsListing =
        {
            "Publicación sospechosa",
            "Posible información falsa",
            "Contenido duplicado",
            "No cumple políticas",
            "Precio engañoso"
        };

        private static readonly string[] ReportReasonsUser =
        {
            "Lenguaje ofensivo",
            "Comportamiento indebido",
            "Intento de estafa",
            "Spam en chat",
            "Acoso en mensajes"
        };

        // Seed inicial para entorno de desarrollo.
        // Crea categorías, publicaciones, imágenes, chats, mensajes y reportes
        // con datos de prueba y relaciones válidas.
        public static async Task Seed(ApplicationDbContext context)
        {

            // Si ya hay listings, no sembramos otra vez
            if (await context.Listings.AnyAsync())
            {
                Console.WriteLine("DB SEED: ya hay datos, saliendo");
                return;
            }

            // Primero dejamos categorías listas, aunque aún no existan usuarios
            var categories = await context.Categories.ToListAsync();
            if (!categories.Any())
            {
                categories = new List<Category>
                {
                    new Category { Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), Name = "Electrónica" },
                    new Category { Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), Name = "Hogar" },
                    new Category { Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), Name = "Ropa" },
                    new Category { Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), Name = "Deportes" },
                    new Category { Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"), Name = "Libros" }
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            // Ahora sí validamos usuarios
            var users = await context.Users.Take(10).ToListAsync();
            Console.WriteLine($"DB SEED: users encontrados = {users.Count}");

            // Si no hay suficientes, salimos sin romper la app
            if (users.Count < 3)
            {
                return;
            }

            var user1 = users[0];
            var user2 = users[1];

            // Listings (15)
            var listings = new List<Listing>();
            for (int i = 1; i <= 15; i++)
            {
                var owner = (i % 2 == 0) ? user1 : user2;
                var category = categories[(i - 1) % categories.Count];

                listings.Add(new Listing
                {
                    Listing_id = Guid.Parse($"00000000-0000-0000-0000-{i.ToString().PadLeft(12, '0')}"),
                    Title = ListingTitles[(i - 1) % ListingTitles.Length],
                    Description = ListingDescriptions[(i - 1) % ListingDescriptions.Length],
                    Price = 10000 + (i * 2500),
                    IsHidden = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-i),
                    User_Id = owner.Id,
                    Category_Id = category.Category_Id
                });
            }

            context.Listings.AddRange(listings);
            await context.SaveChangesAsync();

            // Imágenes (35)
            var images = new List<ListingImage>();
            for (int i = 1; i <= 35; i++)
            {
                var listing = listings[(i - 1) % listings.Count];

                images.Add(new ListingImage
                {
                    ListingImage_Id = Guid.Parse($"10000000-0000-0000-0000-{i.ToString().PadLeft(12, '0')}"),
                    Listing_Id = listing.Listing_id,
                    ImageUrl = $"https://cdn.ptruequeu.demo/listings/{listing.Listing_id}/img-{i}.jpg"
                });
            }

            context.ListingImages.AddRange(images);
            await context.SaveChangesAsync();

            // Chats (20)
            var threads = new List<ChatThread>();
            var allUserIds = users.Select(u => u.Id).ToList();
            int serial = 1;

            foreach (var listing in listings)
            {
                var possibleBuyers = allUserIds.Where(id => id != listing.User_Id).ToList();

                foreach (var buyerId in possibleBuyers)
                {
                    if (threads.Count >= 20) break;

                    bool existsInMemory = threads.Any(t =>
                        t.Listing_Id == listing.Listing_id &&
                        t.Buyer_Id == buyerId &&
                        t.Seller_Id == listing.User_Id);

                    bool existsInDb = await context.ChatThreads.AnyAsync(t =>
                        t.Listing_Id == listing.Listing_id &&
                        t.Buyer_Id == buyerId &&
                        t.Seller_Id == listing.User_Id);

                    if (existsInMemory || existsInDb) continue;

                    threads.Add(new ChatThread
                    {
                        ChatThread_Id = Guid.Parse($"20000000-0000-0000-0000-{serial.ToString().PadLeft(12, '0')}"),
                        Listing_Id = listing.Listing_id,
                        Buyer_Id = buyerId,
                        Seller_Id = listing.User_Id,
                        CreatedAt = DateTime.UtcNow.AddHours(-serial)
                    });

                    serial++;
                }

                if (threads.Count >= 20) break;
            }

            if (threads.Count < 20)
                throw new Exception($"No se pudieron generar 20 chats únicos. Se generaron {threads.Count}.");

            context.ChatThreads.AddRange(threads);
            await context.SaveChangesAsync();

            // Mensajes (30)
            var messages = new List<ChatMessage>();
            for (int i = 1; i <= 30; i++)
            {
                var thread = threads[(i - 1) % threads.Count];
                var senderId = (i % 2 == 0) ? thread.Buyer_Id : thread.Seller_Id;

                messages.Add(new ChatMessage
                {
                    ChatMessage_Id = Guid.Parse($"30000000-0000-0000-0000-{i.ToString().PadLeft(12, '0')}"),
                    Thread_Id = thread.ChatThread_Id,
                    Sender_Id = senderId,
                    Text = MessageSamples[(i - 1) % MessageSamples.Length],
                    SentAt = DateTime.UtcNow.AddMinutes(-i * 3)
                });
            }

            context.ChatMessages.AddRange(messages);
            await context.SaveChangesAsync();

            // Reportes (10)
            var reports = new List<Report>();
            for (int i = 1; i <= 10; i++)
            {
                var reportListing = (i % 2 == 0);
                var listing = listings[(i - 1) % listings.Count];

                reports.Add(new Report
                {
                    Report_Id = Guid.Parse($"40000000-0000-0000-0000-{i.ToString().PadLeft(12, '0')}"),
                    Reporter_Id = user1.Id,
                    ReportedUser_Id = reportListing ? null : user2.Id,
                    ReportedListing_Id = reportListing ? listing.Listing_id : null,
                    Reason = reportListing
                        ? ReportReasonsListing[(i - 1) % ReportReasonsListing.Length]
                        : ReportReasonsUser[(i - 1) % ReportReasonsUser.Length],
                    Comment = $"Reporte generado por seed #{i}",
                    CreatedAt = DateTime.UtcNow.AddDays(-i)
                });
            }

            context.Reports.AddRange(reports);
            await context.SaveChangesAsync();
        }
    }
}
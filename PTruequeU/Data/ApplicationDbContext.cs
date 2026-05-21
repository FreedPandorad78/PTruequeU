using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTruequeU.Models;

namespace PTruequeU.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }
        public DbSet<ChatThread> ChatThreads { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ModerationAction> ModerationActions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // PKs
            builder.Entity<Category>().HasKey(c => c.Category_Id);
            builder.Entity<Listing>().HasKey(l => l.Listing_id);
            builder.Entity<ListingImage>().HasKey(i => i.ListingImage_Id);
            builder.Entity<ChatThread>().HasKey(t => t.ChatThread_Id);
            builder.Entity<ChatMessage>().HasKey(m => m.ChatMessage_Id);
            builder.Entity<Favorite>().HasKey(f => f.Favorite_Id);
            builder.Entity<Report>().HasKey(r => r.Report_Id);
            builder.Entity<ModerationAction>().HasKey(a => a.ModerationAction_Id);

            // Listing -> Category
            builder.Entity<Listing>()
                .HasOne(l => l.Category)
                .WithMany(c => c.Listings)
                .HasForeignKey(l => l.Category_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Listing -> User
            builder.Entity<Listing>()
                .HasOne(l => l.User)
                .WithMany(u => u.Listings)
                .HasForeignKey(l => l.User_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Listing.Price
            builder.Entity<Listing>()
                .Property(l => l.Price)
                .HasColumnType("decimal(18,2)");

            // ListingImage -> Listing
            builder.Entity<ListingImage>()
                .HasOne(i => i.Listing)
                .WithMany(l => l.Images)
                .HasForeignKey(i => i.Listing_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // ChatThread -> Listing
            builder.Entity<ChatThread>()
                .HasOne(t => t.Listing)
                .WithMany()
                .HasForeignKey(t => t.Listing_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // ChatThread -> Buyer
            builder.Entity<ChatThread>()
                .HasOne(t => t.Buyer)
                .WithMany()
                .HasForeignKey(t => t.Buyer_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ChatThread -> Seller
            builder.Entity<ChatThread>()
                .HasOne(t => t.Seller)
                .WithMany()
                .HasForeignKey(t => t.Seller_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ChatMessage -> Thread
            builder.Entity<ChatMessage>()
                .HasOne(m => m.Thread)
                .WithMany(t => t.Messages)
                .HasForeignKey(m => m.Thread_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // ChatMessage -> Sender
            builder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.Sender_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Un chat único por combinación Listing + Buyer + Seller
            builder.Entity<ChatThread>()
                .HasIndex(t => new { t.Listing_Id, t.Buyer_Id, t.Seller_Id })
                .IsUnique();

            // Favorite -> Listing
            builder.Entity<Favorite>()
                .HasOne(f => f.Listing)
                .WithMany()
                .HasForeignKey(f => f.Listing_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Favorite -> User
            builder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique favorite per user + listing
            builder.Entity<Favorite>()
                .HasIndex(f => new { f.User_Id, f.Listing_Id })
                .IsUnique();

            // Report -> Reporter
            builder.Entity<Report>()
                .HasOne(r => r.Reporter)
                .WithMany()
                .HasForeignKey(r => r.Reporter_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Report -> ReportedUser
            builder.Entity<Report>()
                .HasOne(r => r.ReportedUser)
                .WithMany()
                .HasForeignKey(r => r.ReportedUser_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Report -> ReportedListing
            builder.Entity<Report>()
                .HasOne(r => r.ReportedListing)
                .WithMany()
                .HasForeignKey(r => r.ReportedListing_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ModerationAction -> Admin
            builder.Entity<ModerationAction>()
                .HasOne(a => a.Admin)
                .WithMany()
                .HasForeignKey(a => a.Admin_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Indices
            builder.Entity<Report>().HasIndex(r => r.Reporter_Id);
            builder.Entity<Report>().HasIndex(r => r.ReportedUser_Id);
            builder.Entity<Report>().HasIndex(r => r.ReportedListing_Id);
            builder.Entity<Report>().HasIndex(r => r.CreatedAt);
            builder.Entity<ModerationAction>().HasIndex(a => a.Admin_Id);
            builder.Entity<ModerationAction>().HasIndex(a => a.TargetId);
            builder.Entity<ModerationAction>().HasIndex(a => a.CreatedAt);

            // ===================== SEED =====================

            // Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "11111111-aaaa-1111-aaaa-111111111111"
                },
                new IdentityRole
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "22222222-bbbb-2222-bbbb-222222222222"
                }
            );

            builder.Entity<ApplicationUser>().HasData(
      new ApplicationUser
      {
          Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000000",
          UserName = "admin",
          NormalizedUserName = "ADMIN",
          Email = "admin@email.com",
          NormalizedEmail = "ADMIN@EMAIL.COM",
          EmailConfirmed = true,
          IsSuspended = false,
          Rating = 0.0,
          SecurityStamp = "SECSTAMP-STATIC-0000000000000000",
          ConcurrencyStamp = "CONCSTAMP-STATIC-000000000000000",
          PasswordHash = "AQAAAAIAAYagAAAAENx4zxiRrAfPtkZV5vFfJ+RgEHgHrAvRjf6bdYMLzInq/XWu7gibD/0GBKoPO3EBEg=="
      },
      new ApplicationUser
      {
          Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001",
          UserName = "andrea",
          NormalizedUserName = "ANDREA",
          Email = "andrea@email.com",
          NormalizedEmail = "ANDREA@EMAIL.COM",
          EmailConfirmed = true,
          SecurityStamp = "SECSTAMP-STATIC-0000000000000001",
          ConcurrencyStamp = "CONCSTAMP-STATIC-000000000000001",
          PasswordHash = "AQAAAAIAAYagAAAAENx4zxiRrAfPtkZV5vFfJ+RgEHgHrAvRjf6bdYMLzInq/XWu7gibD/0GBKoPO3EBEg=="
      },
      new ApplicationUser
      {
          Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002",
          UserName = "bruno",
          NormalizedUserName = "BRUNO",
          Email = "bruno@email.com",
          NormalizedEmail = "BRUNO@EMAIL.COM",
          EmailConfirmed = true,
          SecurityStamp = "SECSTAMP-STATIC-0000000000000002",
          ConcurrencyStamp = "CONCSTAMP-STATIC-000000000000002",
          PasswordHash = "AQAAAAIAAYagAAAAENx4zxiRrAfPtkZV5vFfJ+RgEHgHrAvRjf6bdYMLzInq/XWu7gibD/0GBKoPO3EBEg=="
      },
      new ApplicationUser
      {
          Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003",
          UserName = "carla",
          NormalizedUserName = "CARLA",
          Email = "carla@email.com",
          NormalizedEmail = "CARLA@EMAIL.COM",
          EmailConfirmed = true,
          SecurityStamp = "SECSTAMP-STATIC-0000000000000003",
          ConcurrencyStamp = "CONCSTAMP-STATIC-000000000000003",
          PasswordHash = "AQAAAAIAAYagAAAAENx4zxiRrAfPtkZV5vFfJ+RgEHgHrAvRjf6bdYMLzInq/XWu7gibD/0GBKoPO3EBEg=="
      }
  );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "aaaaaaaa-aaaa-aaaa-aaaa-000000000000",
                    RoleId = "11111111-1111-1111-1111-111111111111"
                }
            );

            // Categorias
            builder.Entity<Category>().HasData(
                new Category { Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), Name = "Electrónica" },
                new Category { Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"), Name = "Hogar" },
                new Category { Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"), Name = "Ropa" },
                new Category { Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"), Name = "Deportes" },
                new Category { Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000005"), Name = "Libros" }
            );

            // Listings (15)
            builder.Entity<Listing>().HasData(
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000001"), Title = "Celular Samsung Galaxy A54", Description = "Excelente estado, cargador original incluido.", Price = 120000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000002"), Title = "Laptop HP Pavilion 15", Description = "Intel i5, 8GB RAM, SSD 256GB, sin golpes.", Price = 350000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), CreatedAt = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000003"), Title = "Audífonos Sony WH-1000XM4", Description = "Cancelación de ruido activa, poco uso.", Price = 95000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), CreatedAt = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000004"), Title = "Tablet Lenovo Tab M10", Description = "Pantalla 10 pulgadas, 64GB, con funda protectora.", Price = 85000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000001"), CreatedAt = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000005"), Title = "Silla gamer RGB", Description = "Ergonómica, reposapiés incluido, color negro/rojo.", Price = 80000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"), CreatedAt = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000006"), Title = "Sofá de 3 puestos", Description = "Tela beige, sin manchas, desmontable.", Price = 180000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"), CreatedAt = new DateTime(2023, 1, 6, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000007"), Title = "Cafetera espresso Oster", Description = "Uso doméstico, incluye jarra de leche.", Price = 45000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000002"), CreatedAt = new DateTime(2023, 1, 7, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 7, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000008"), Title = "Chaqueta de cuero negra", Description = "Talla M, muy poco uso, estilo biker.", Price = 60000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"), CreatedAt = new DateTime(2023, 1, 8, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 8, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000009"), Title = "Jeans Levi's 511", Description = "Talla 32x32, corte slim, azul oscuro.", Price = 35000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"), CreatedAt = new DateTime(2023, 1, 9, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 9, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000010"), Title = "Zapatillas Nike Air Max 90", Description = "Talla 42, usadas 3 veces, blanco/gris.", Price = 70000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000003"), CreatedAt = new DateTime(2023, 1, 10, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 10, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000011"), Title = "Bicicleta de montaña Trek", Description = "Rodada 29, frenos hidráulicos, 21 velocidades.", Price = 250000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"), CreatedAt = new DateTime(2023, 1, 11, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 11, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000012"), Title = "Mancuernas ajustables 20kg", Description = "Par completo con barra y discos, recubrimiento goma.", Price = 55000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"), CreatedAt = new DateTime(2023, 1, 12, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000013"), Title = "Raqueta de tenis Wilson", Description = "Head 100, grip 4, en funda original.", Price = 40000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000004"), CreatedAt = new DateTime(2023, 1, 13, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 13, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000014"), Title = "Libro Clean Code", Description = "Edición en español, marcas de lápiz leves.", Price = 25000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000005"), CreatedAt = new DateTime(2023, 1, 14, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 14, 0, 0, 0, DateTimeKind.Utc) },
                new Listing { Listing_id = new Guid("cccccccc-cccc-cccc-cccc-000000000015"), Title = "Libro C# en Profundidad", Description = "4ta edición, como nuevo, sin subrayados.", Price = 30000, IsHidden = false, User_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Category_Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-000000000005"), CreatedAt = new DateTime(2023, 1, 15, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2023, 1, 15, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Imagenes (35)
            builder.Entity<ListingImage>().HasData(
            // Samsung Galaxy A54 (001, 002, 003, 035)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000001"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000001"), ImageUrl = "https://images.unsplash.com/photo-1610945415295-d9bbf067e59c?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000002"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000001"), ImageUrl = "https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000003"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000001"), ImageUrl = "https://images.unsplash.com/photo-1592750475338-74b7b21085ab?w=400" },
            // Laptop HP (004, 005, 006)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000004"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000002"), ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000005"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000002"), ImageUrl = "https://images.unsplash.com/photo-1517694712202-14dd9538aa97?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000006"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000002"), ImageUrl = "https://images.unsplash.com/photo-1525547719571-a2d4ac8945e2?w=400" },
            // Audífonos Sony (007, 008)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000007"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000003"), ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000008"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000003"), ImageUrl = "https://images.unsplash.com/photo-1583394838336-acd977736f90?w=400" },
            // Tablet Lenovo (009, 010)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000009"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000004"), ImageUrl = "https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000010"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000004"), ImageUrl = "https://images.unsplash.com/photo-1561154464-82e9adf32764?w=400" },
            // Silla gamer (011, 012)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000011"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000005"), ImageUrl = "https://images.unsplash.com/photo-1598550476439-6847785fcea6?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000012"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000005"), ImageUrl = "https://images.unsplash.com/photo-1605296867424-35fc25c9212a?w=400" },
            // Sofá (013, 014)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000013"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000006"), ImageUrl = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000014"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000006"), ImageUrl = "https://images.unsplash.com/photo-1493663284031-b7e3aefcae8e?w=400" },
            // Cafetera (015, 016)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000015"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000007"), ImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000016"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000007"), ImageUrl = "https://images.unsplash.com/photo-1514432324607-a09d9b4aefdd?w=400" },
            // Chaqueta (017, 018)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000017"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000008"), ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000018"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000008"), ImageUrl = "https://images.unsplash.com/photo-1558769132-cb1aea458c5e?w=400" },
            // Jeans (019, 020)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000019"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000009"), ImageUrl = "https://images.unsplash.com/photo-1542272604-787c3835535d?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000020"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000009"), ImageUrl = "https://images.unsplash.com/photo-1475178626620-a4d074967452?w=400" },
            // Nike Air Max (021, 022)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000021"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000010"), ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000022"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000010"), ImageUrl = "https://images.unsplash.com/photo-1606107557195-0e29a4b5b4aa?w=400" },
            // Bicicleta Trek (023, 024, 025)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000023"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000011"), ImageUrl = "https://images.unsplash.com/photo-1485965120184-e220f721d03e?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000024"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000011"), ImageUrl = "https://images.unsplash.com/photo-1532298229144-0ec0c57515c7?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000025"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000011"), ImageUrl = "https://images.unsplash.com/photo-1511994298241-608e28f14fde?w=400" },
            // Mancuernas (026, 027)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000026"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000012"), ImageUrl = "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000027"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000012"), ImageUrl = "https://images.unsplash.com/photo-1517836357463-d25dfeac3438?w=400" },
            // Raqueta Wilson (028, 029)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000028"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000013"), ImageUrl = "https://images.unsplash.com/photo-1617083934551-ac453d8f7097?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000029"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000013"), ImageUrl = "https://images.unsplash.com/photo-1551773188-0801da12ddae?w=400" },
            // Clean Code (030, 031)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000030"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000014"), ImageUrl = "https://images.unsplash.com/photo-1532012197267-da84d127e765?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000031"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000014"), ImageUrl = "https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=400" },
            // C# en Profundidad (032, 033, 034)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000032"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000015"), ImageUrl = "https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000033"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000015"), ImageUrl = "https://images.unsplash.com/photo-1515879218367-8466d910aaa4?w=400" },
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000034"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000015"), ImageUrl = "https://images.unsplash.com/photo-1542831371-29b0f74f9713?w=400" },
            // Samsung extra (035)
            new ListingImage { ListingImage_Id = new Guid("dddddddd-dddd-dddd-dddd-000000000035"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000001"), ImageUrl = "https://images.unsplash.com/photo-1565849904461-04a58ad377e0?w=400" }
            );

            // Chats (20)
            builder.Entity<ChatThread>().HasData(
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000001"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000001"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000002"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000001"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", CreatedAt = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000003"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000002"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", CreatedAt = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000004"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000002"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", CreatedAt = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000005"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000003"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", CreatedAt = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000006"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000004"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", CreatedAt = new DateTime(2023, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000007"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000005"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", CreatedAt = new DateTime(2023, 1, 7, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000008"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000005"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", CreatedAt = new DateTime(2023, 1, 8, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000009"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000006"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", CreatedAt = new DateTime(2023, 1, 9, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000010"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000006"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", CreatedAt = new DateTime(2023, 1, 10, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000011"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000007"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", CreatedAt = new DateTime(2023, 1, 11, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000012"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000008"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", CreatedAt = new DateTime(2023, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000013"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000008"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", CreatedAt = new DateTime(2023, 1, 13, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000014"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000009"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", CreatedAt = new DateTime(2023, 1, 14, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000015"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000010"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", CreatedAt = new DateTime(2023, 1, 15, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000016"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000011"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", CreatedAt = new DateTime(2023, 1, 16, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000017"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000011"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", CreatedAt = new DateTime(2023, 1, 17, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000018"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000012"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", CreatedAt = new DateTime(2023, 1, 18, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000019"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000014"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", CreatedAt = new DateTime(2023, 1, 19, 0, 0, 0, DateTimeKind.Utc) },
     new ChatThread { ChatThread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000020"), Listing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000015"), Buyer_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Seller_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", CreatedAt = new DateTime(2023, 1, 20, 0, 0, 0, DateTimeKind.Utc) }
 );

            // Mensajes (30)
            builder.Entity<ChatMessage>().HasData(
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000001"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000001"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "Hola, ¿el celular sigue disponible?", SentAt = new DateTime(2023, 1, 10, 10, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000002"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000001"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "Sí, está disponible. ¿Tienes alguna duda?", SentAt = new DateTime(2023, 1, 10, 10, 5, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000003"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000001"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "¿Aceptas algo menos de precio?", SentAt = new DateTime(2023, 1, 10, 10, 8, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000004"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000002"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Text = "¿El Samsung tiene algún golpe o rallón?", SentAt = new DateTime(2023, 1, 11, 9, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000005"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000002"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "Ninguno, está impecable.", SentAt = new DateTime(2023, 1, 11, 9, 3, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000006"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000003"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "¿Cuál es la autonomía de batería de la laptop?", SentAt = new DateTime(2023, 1, 12, 14, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000007"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000003"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "Unas 5 horas de uso normal.", SentAt = new DateTime(2023, 1, 12, 14, 10, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000008"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000004"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Text = "¿La laptop tiene Windows original instalado?", SentAt = new DateTime(2023, 1, 13, 8, 30, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000009"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000004"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "Sí, Windows 11 activado y sin problemas.", SentAt = new DateTime(2023, 1, 13, 8, 35, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000010"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000005"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Text = "¿Los audífonos vienen con estuche original?", SentAt = new DateTime(2023, 1, 14, 11, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000011"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000005"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "Sí, estuche, cable y adaptador incluidos.", SentAt = new DateTime(2023, 1, 14, 11, 6, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000012"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000007"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "¿La silla soporta más de 100kg?", SentAt = new DateTime(2023, 1, 15, 16, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000013"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000007"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "Sí, hasta 130kg según el fabricante.", SentAt = new DateTime(2023, 1, 15, 16, 4, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000014"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000008"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Text = "¿Puedes bajar un poco el precio de la silla?", SentAt = new DateTime(2023, 1, 16, 10, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000015"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000008"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "Te la dejo en 75.000 si cerramos hoy.", SentAt = new DateTime(2023, 1, 16, 10, 7, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000016"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000009"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "¿El sofá se puede desmontar para transportarlo?", SentAt = new DateTime(2023, 1, 17, 13, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000017"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000009"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "Sí, en partes cabe sin problema en un auto mediano.", SentAt = new DateTime(2023, 1, 17, 13, 5, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000018"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000011"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "¿Cuánto tiempo tiene de uso la cafetera?", SentAt = new DateTime(2023, 1, 18, 9, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000019"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000011"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "La compré hace 4 meses, tiene muy poco uso.", SentAt = new DateTime(2023, 1, 18, 9, 4, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000020"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000012"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "¿La chaqueta es de cuero genuino o sintético?", SentAt = new DateTime(2023, 1, 19, 10, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000021"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000012"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Text = "Es cuero genuino, tengo la factura original.", SentAt = new DateTime(2023, 1, 19, 10, 5, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000022"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000016"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Text = "¿La bici tiene cambios Shimano?", SentAt = new DateTime(2023, 1, 20, 15, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000023"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000016"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "Sí, Shimano Altus de 21 velocidades.", SentAt = new DateTime(2023, 1, 20, 15, 3, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000024"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000017"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Text = "¿Hacen envío o solo retiro en persona?", SentAt = new DateTime(2023, 1, 21, 10, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000025"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000017"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "Solo retiro, la bici es muy voluminosa.", SentAt = new DateTime(2023, 1, 21, 10, 6, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000026"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000018"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "¿Las mancuernas son de hierro fundido?", SentAt = new DateTime(2023, 1, 22, 8, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000027"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000018"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Text = "Sí, con recubrimiento de goma antideslizante.", SentAt = new DateTime(2023, 1, 22, 8, 5, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000028"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000019"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Text = "¿Clean Code trae ejercicios prácticos?", SentAt = new DateTime(2023, 1, 23, 11, 0, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000029"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000019"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "Tiene ejemplos de código reales en cada capítulo.", SentAt = new DateTime(2023, 1, 23, 11, 8, 0, DateTimeKind.Utc) },
                new ChatMessage { ChatMessage_Id = new Guid("ffffffff-ffff-ffff-ffff-000000000030"), Thread_Id = new Guid("eeeeeeee-eeee-eeee-eeee-000000000020"), Sender_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Text = "¿El libro de C# cubre .NET 6 o superior?", SentAt = new DateTime(2023, 1, 24, 14, 0, 0, DateTimeKind.Utc) }
            );

            // Reportes (10)
            builder.Entity<Report>().HasData(
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000001"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", ReportedListing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000001"), Reason = "Precio sospechoso", Comment = "El precio es demasiado bajo para ese modelo, posible robo.", CreatedAt = new DateTime(2023, 1, 12, 10, 0, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000002"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", ReportedUser_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", Reason = "Intento de estafa", Comment = "Me pidió hacer la transferencia fuera de la plataforma.", CreatedAt = new DateTime(2023, 1, 14, 11, 30, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000003"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", ReportedListing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000005"), Reason = "Publicación duplicada", Comment = "Hay 3 publicaciones idénticas de este usuario.", CreatedAt = new DateTime(2023, 1, 16, 9, 0, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000004"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", ReportedListing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000002"), Reason = "Descripción engañosa", Comment = "Las fotos no corresponden al producto que se entrega.", CreatedAt = new DateTime(2023, 1, 18, 14, 0, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000005"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", ReportedUser_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", Reason = "Comportamiento inapropiado", Comment = "Envió mensajes ofensivos durante la negociación del precio.", CreatedAt = new DateTime(2023, 1, 20, 8, 45, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000006"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", ReportedListing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000008"), Reason = "Artículo falsificado", Comment = "La chaqueta tiene logos de marca que parecen réplica.", CreatedAt = new DateTime(2023, 1, 22, 16, 0, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000007"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", ReportedListing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000011"), Reason = "Producto ya vendido", Comment = "El vendedor confirmó que fue vendido pero la publicación sigue.", CreatedAt = new DateTime(2023, 1, 25, 10, 15, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000008"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000001", ReportedListing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000014"), Reason = "Derechos de autor", Comment = "Parece ser una copia no autorizada, no el original.", CreatedAt = new DateTime(2023, 1, 27, 13, 0, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000009"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000002", ReportedUser_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", Reason = "Cuenta sospechosa", Comment = "El perfil parece ser un bot, respuestas automáticas y rápidas.", CreatedAt = new DateTime(2023, 1, 29, 9, 30, 0, DateTimeKind.Utc) },
                new Report { Report_Id = new Guid("11111111-1111-1111-1111-000000000010"), Reporter_Id = "aaaaaaaa-aaaa-aaaa-aaaa-000000000003", ReportedListing_Id = new Guid("cccccccc-cccc-cccc-cccc-000000000015"), Reason = "Precio inflado", Comment = "El mismo libro nuevo cuesta menos en cualquier librería física.", CreatedAt = new DateTime(2023, 2, 1, 17, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}
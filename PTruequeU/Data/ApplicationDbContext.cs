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

            // PKs (porque usas nombres custom)
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

            // Unique chat thread per listing + buyer + seller
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

            // Índices útiles
            builder.Entity<Report>().HasIndex(r => r.Reporter_Id);
            builder.Entity<Report>().HasIndex(r => r.ReportedUser_Id);
            builder.Entity<Report>().HasIndex(r => r.ReportedListing_Id);
            builder.Entity<Report>().HasIndex(r => r.CreatedAt);

            builder.Entity<ModerationAction>().HasIndex(a => a.Admin_Id);
            builder.Entity<ModerationAction>().HasIndex(a => a.TargetId);
            builder.Entity<ModerationAction>().HasIndex(a => a.CreatedAt);

          
            // SEED 
            // 1) Roles
            var adminRoleId = "11111111-1111-1111-1111-111111111111";
            var userRoleId = "22222222-2222-2222-2222-222222222222";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "11111111-aaaa-1111-aaaa-111111111111"
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "22222222-bbbb-2222-bbbb-222222222222"
                }
            );

            // 2) Categorías base
            builder.Entity<Category>().HasData(
                new Category
                {
                    Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                    Name = "Electrónica"
                },
                new Category
                {
                    Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                    Name = "Hogar"
                },
                new Category
                {
                    Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
                    Name = "Ropa"
                },
                new Category
                {
                    Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
                    Name = "Deportes"
                },
                new Category
                {
                    Category_Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"),
                    Name = "Libros"
                }
            );
        }
    }
}
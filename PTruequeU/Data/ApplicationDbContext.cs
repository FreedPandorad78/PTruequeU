using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTruequeU.Models;
using System.Reflection.Emit;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // PKs (porque usas nombres custom)
            builder.Entity<Category>().HasKey(c => c.Category_Id);
            builder.Entity<Listing>().HasKey(l => l.Listing_id);
            builder.Entity<ListingImage>().HasKey(i => i.ListingImage_Id);

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

            // ListingImage -> Listing
            builder.Entity<ListingImage>()
                .HasOne(i => i.Listing)
                .WithMany(l => l.Images)
                .HasForeignKey(i => i.Listing_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChatThread>().HasKey(t => t.ChatThread_Id);
            builder.Entity<ChatMessage>().HasKey(m => m.ChatMessage_Id);

            builder.Entity<ChatThread>()
                .HasOne(t => t.Listing)
                .WithMany()
                .HasForeignKey(t => t.Listing_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChatThread>()
                .HasOne(t => t.Buyer)
                .WithMany()
                .HasForeignKey(t => t.Buyer_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChatThread>()
                .HasOne(t => t.Seller)
                .WithMany()
                .HasForeignKey(t => t.Seller_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChatMessage>()
                .HasOne(m => m.Thread)
                .WithMany(t => t.Messages)
                .HasForeignKey(m => m.Thread_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.Sender_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChatThread>()
                .HasIndex(t => new { t.Listing_Id, t.Buyer_Id, t.Seller_Id })
                .IsUnique();
        }
    }
}
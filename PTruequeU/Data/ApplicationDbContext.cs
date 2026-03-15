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
        }
    }
}
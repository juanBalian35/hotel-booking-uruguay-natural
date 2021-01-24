using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Context
{
    public class UruguayNaturalContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<TouristSpot> TouristSpots { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<LodgingReview> LodgingReviews { get; set; }

        public UruguayNaturalContext() { }
        public UruguayNaturalContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Administrator>()
                .HasIndex(a => a.Email)
                .IsUnique();

            builder.Entity<Administrator>()
                .HasMany(a => a.Sessions)
                .WithOne(c => c.Administrator)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Region>()
               .HasIndex(r => r.Name)
               .IsUnique();

            builder.Entity<Category>()
               .HasIndex(c => c.Name)
               .IsUnique();

            builder.Entity<Session>()
                .HasKey(s => s.Token);

            builder.Entity<TouristSpot>()
                .Property(t => t.Description)
                .HasMaxLength(2000);

            builder.Entity<TouristSpot>()
                .HasOne(ts => ts.Region)
                .WithMany(r => r.TouristSpots)
                .HasForeignKey(ts => ts.RegionId);

            builder.Entity<TouristSpot>()
                .HasMany(ts => ts.Lodgings)
                .WithOne(l => l.TouristSpot);
            
            builder.Entity<TouristSpotCategory>()
                .HasKey(tsc => new { tsc.CategoryId, tsc.TouristSpotId });

            builder.Entity<TouristSpotCategory>()
                .HasOne(bc => bc.TouristSpot)
                .WithMany(b => b.TouristSpotCategories)
                .HasForeignKey(bc => bc.TouristSpotId);

            builder.Entity<TouristSpotCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.TouristSpotCateogries)
                .HasForeignKey(bc => bc.CategoryId);

            builder.Entity<Lodging>()
                .Ignore(l => l.TotalPrice);

            builder.Entity<Lodging>()
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);
            
            builder.Entity<Lodging>()
                .HasMany(l => l.Images)
                .WithOne(li => li.Lodging);

            builder.Entity<Booking>()
                .HasOne(b => b.Tourist);

            builder.Entity<LodgingReview>()
                .HasOne(b => b.Booking);
            
            builder.Entity<LodgingReview>()
                .HasIndex(l => l.BookingId)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString(@"UruguayNaturalDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}

using Entites;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Entites.Enums;

namespace Data
{
    public class FarmBotDbContext : DbContext
    {
        public DbSet<FarmBot> FarmBots { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<Event> Events { get; set; }

        public FarmBotDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FarmBots
            modelBuilder.Entity<FarmBot>()
                .Property(p => p.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<FarmBot>()
                .Property(p => p.Updated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<FarmBot>()
                .HasIndex(p => p.Name)
                .IsUnique();

            // Current parameters
            modelBuilder.Entity<Parameters>()
                .Property(p => p.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Parameters>()
                .Ignore(p => p.Updated);

            // Plants
            modelBuilder.Entity<Plant>()
                .Property(p => p.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Plant>()
                .Property(p => p.Updated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Plant>()
                .HasIndex(p => p.Name)
                .IsUnique();

            // User settings
            modelBuilder.Entity<Settings>()
                .Property(p => p.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Settings>()
                .Property(p => p.Updated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Settings>()
                .HasIndex(p => p.UserId)
                .IsUnique();

            // Events
            modelBuilder.Entity<Event>()
                .Property(p => p.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Event>()
                .Ignore(p => p.Updated);

            modelBuilder.Entity<Event>()
                .Property(e => e.Type)
                .HasConversion(new EnumToNumberConverter<EventType, int>());

            base.OnModelCreating(modelBuilder);
        }
    }
}

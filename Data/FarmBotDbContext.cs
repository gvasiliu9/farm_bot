using Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using static Entities.Enums;

namespace Data
{
    public class FarmBotDbContext : DbContext
    {
        public DbSet<FarmBot> FarmBots { get; set; }

        public DbSet<FarmBotPlant> FarmBotPlants { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<Parameters> Parameters { get; set; }

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

            modelBuilder.Entity<FarmBot>()
                .HasData(new FarmBot {
                    Id = Guid.Parse("99d9742b-1ee2-45c9-a9fb-8742baa3bb86"),
                    Name = "Utm FarmBot",
                    IpAddress = "192.168.1.112",
                    IpCameraAddress = "http://192.168.1.1:8080/video"
                });

            // FarmBotPlants
            modelBuilder.Entity<FarmBotPlant>()
                .Ignore(p => p.Id);

            modelBuilder.Entity<FarmBotPlant>()
                .HasKey(fbp => new {fbp.FarmBotId, fbp.PlantId });

            modelBuilder.Entity<FarmBotPlant>()
                .HasOne(p => p.FarmBot)
                .WithMany(p => p.FarmBotPlants)
                .HasForeignKey(p => p.FarmBotId);

            modelBuilder.Entity<FarmBotPlant>()
                .HasOne(p => p.Plant)
                .WithMany(p => p.FarmBotPlants)
                .HasForeignKey(p => p.PlantId);

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

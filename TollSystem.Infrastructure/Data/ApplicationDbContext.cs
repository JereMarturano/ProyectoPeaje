using Microsoft.EntityFrameworkCore;
using TollSystem.Domain.Entities;
using TollSystem.Domain.ValueObjects;

namespace TollSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TollPassage> TollPassages { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LicensePlate)
                    .HasConversion(
                        v => v.Value,
                        v => new LicensePlate(v))
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.Color).HasMaxLength(50);
                entity.Property(e => e.Axles).IsRequired();
                entity.Property(e => e.Height).HasColumnType("decimal(18,2)");
                entity.Property(e => e.VehicleCategoryId);
            });

            modelBuilder.Entity<TollPassage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.AmountCharged).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Vehicle)
                    .WithMany()
                    .HasForeignKey(e => e.VehicleId);
            });

            modelBuilder.Entity<Tariff>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.VehicleCategory);
            });
        }
    }
}

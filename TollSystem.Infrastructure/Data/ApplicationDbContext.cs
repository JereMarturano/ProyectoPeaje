using Microsoft.EntityFrameworkCore;
using TollSystem.Domain.Entities;
using TollSystem.Domain.ValueObjects;
using TollSystem.Domain.Enums; 

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

           
            modelBuilder.Entity<Tariff>().HasData(
                new { Id = 1, VehicleCategory = VehicleCategory.Moto, Price = 600m },
                new { Id = 2, VehicleCategory = VehicleCategory.Auto, Price = 2000m },
                new { Id = 3, VehicleCategory = VehicleCategory.CamionPesado2Ejes, Price = 4000m },
                new { Id = 4, VehicleCategory = VehicleCategory.Camion3a4Ejes, Price = 11700m },
                new { Id = 5, VehicleCategory = VehicleCategory.Camion5a6Ejes, Price = 15600m },
                new { Id = 6, VehicleCategory = VehicleCategory.CamionMas6Ejes, Price = 19500m }
            );
            
        }
    }
}
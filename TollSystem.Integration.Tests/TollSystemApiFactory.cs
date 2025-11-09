using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Enums;
using TollSystem.Infrastructure.Data;

namespace TollSystem.Integration.Tests
{
    public class TollSystemApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    db.Database.EnsureCreated();

                    // Seed the database
                    db.Tariffs.Add(new Tariff(VehicleCategory.Moto, 600));
                    db.Tariffs.Add(new Tariff(VehicleCategory.Auto, 2000));
                    db.Tariffs.Add(new Tariff(VehicleCategory.CamionPesado2Ejes, 4000));
                    db.Tariffs.Add(new Tariff(VehicleCategory.Camion3a4Ejes, 11700));
                    db.Tariffs.Add(new Tariff(VehicleCategory.Camion5a6Ejes, 15600));
                    db.Tariffs.Add(new Tariff(VehicleCategory.CamionMas6Ejes, 19500));
                    db.SaveChanges();
                }
            });
        }
    }
}

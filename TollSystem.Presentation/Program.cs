using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Application.Services;
using TollSystem.Domain.Exceptions;
using TollSystem.Domain.Repositories;
using TollSystem.Infrastructure.Data;
using TollSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TollSystem.Presentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();

                var tollPassageService = services.GetRequiredService<ITollPassageService>();
                var vehicleService = services.GetRequiredService<IVehicleService>();

                try
                {
                    Console.WriteLine("Enter license plate:");
                    var licensePlate = Console.ReadLine();

                    Console.WriteLine("Enter vehicle color:");
                    var color = Console.ReadLine();

                    Console.WriteLine("Enter number of axles:");
                    var axles = int.Parse(Console.ReadLine());

                    var vehicle = await vehicleService.GetOrCreateVehicleAsync(licensePlate, color, axles);
                    var tollPassage = await tollPassageService.CreateTollPassageAsync(vehicle);

                    Console.WriteLine($"Toll passage created with ID: {tollPassage.Id}");
                }
                catch (InvalidLicensePlateFormatException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                    services.AddScoped<IVehicleRepository, VehicleRepository>();
                    services.AddScoped<ITollPassageRepository, TollPassageRepository>();

                    services.AddScoped<IVehicleService, VehicleService>();
                    services.AddScoped<ITollPassageService, TollPassageService>();
                });
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TollSystem.Application.Interfaces;
using TollSystem.Application.Services;
using TollSystem.Domain.Repositories;
using TollSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TollSystem.Infrastructure.Data;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
        builder.Services.AddDbContext<ApplicationDbContext>(options =>                   
            options.UseSqlServer(connectionString));                                    

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        builder.Services.AddScoped<ITollPassageRepository, TollPassageRepository>();
        builder.Services.AddScoped<ITariffRepository, TariffRepository>();
        builder.Services.AddScoped<IVehicleService, VehicleService>();
        builder.Services.AddScoped<ITollPassageService, TollPassageService>();
        builder.Services.AddScoped<ITariffService, TariffService>();
        builder.Services.AddScoped<ITollQueryService, TollQueryService>();

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
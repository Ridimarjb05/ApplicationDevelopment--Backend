using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleParts.Application.Interfaces;
using VehicleParts.Application.Services;
using VehicleParts.Infrastructure.Data;
using VehicleParts.Infrastructure.Identity;
using VehicleParts.Infrastructure.Repositories;
using VehicleParts.Infrastructure.Services;

namespace VehicleParts.Infrastructure;


/// Extension method that wires every Infrastructure and Application service
/// into the DI container. Called once from WebApi/Program.cs.

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration          configuration)
    {
        // Database 
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("Default")));

        //Identity 
        services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
        {
            // Password policy
            opts.Password.RequiredLength         = 8;
            opts.Password.RequireUppercase        = true;
            opts.Password.RequireLowercase        = true;
            opts.Password.RequireDigit            = true;
            opts.Password.RequireNonAlphanumeric  = true;

            // Lockout settings
            opts.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(5);
            opts.Lockout.MaxFailedAccessAttempts = 5;
            opts.Lockout.AllowedForNewUsers      = true;

            // Unique email
            opts.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        // Repositories 
        services.AddScoped<IPartRepository, PartRepository>();

        // Application Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPartService, PartService>();
        services.AddScoped<IStaffService, VehicleParts.Infrastructure.Services.StaffService>();

        return services;
    }
}

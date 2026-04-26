using Microsoft.AspNetCore.Identity;
using VehicleParts.Domain.Enums;
using VehicleParts.Infrastructure.Identity;

namespace VehicleParts.Infrastructure.Seeders;


/// Seeds required roles and a default Admin user on application startup.
/// Safe to call multiple times — checks for existence before inserting.

public static class RoleSeeder
{
    public static async Task SeedAsync(
        RoleManager<IdentityRole>    roleManager,
        UserManager<ApplicationUser> userManager)
    {
        // Seed roles 
        string[] roles = [AppRoles.Admin, AppRoles.Staff, AppRoles.Customer];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        //Seed default Admin user 
        const string adminEmail    = "admin@vehicleparts.com";
        const string adminPassword = "Admin@12345!";

        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new ApplicationUser
            {
                FullName  = "System Administrator",
                UserName  = adminEmail,
                Email     = adminEmail,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
        }
    }
}

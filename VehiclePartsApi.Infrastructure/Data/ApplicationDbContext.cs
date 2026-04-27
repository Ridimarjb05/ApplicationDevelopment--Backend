using Microsoft.EntityFrameworkCore;
using VehiclePartsApi.Domain.Entities;

namespace VehiclePartsApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.CustomerProfile)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.CustomerProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
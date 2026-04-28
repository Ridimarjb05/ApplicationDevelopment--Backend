using Microsoft.EntityFrameworkCore;
using VehicleParts.Domain.Models;

namespace VehicleParts.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Name).IsRequired().HasMaxLength(100);
            entity.Property(v => v.Email).IsRequired().HasMaxLength(150);
            entity.Property(v => v.Phone).IsRequired().HasMaxLength(20);
            entity.Property(v => v.Address).IsRequired().HasMaxLength(250);
            entity.Property(v => v.ContactPerson).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.FirstName).IsRequired().HasMaxLength(60);
            entity.Property(c => c.LastName).IsRequired().HasMaxLength(60);
            entity.Property(c => c.Email).IsRequired().HasMaxLength(150);
            entity.Property(c => c.Phone).IsRequired().HasMaxLength(20);
            entity.Property(c => c.Address).HasMaxLength(250);
            entity.Property(c => c.NIC).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.RegistrationNumber).IsRequired().HasMaxLength(20);
            entity.Property(v => v.Make).IsRequired().HasMaxLength(60);
            entity.Property(v => v.Model).IsRequired().HasMaxLength(60);
            entity.Property(v => v.Color).HasMaxLength(30);
            entity.Property(v => v.VinNumber).HasMaxLength(17);
            entity.HasOne(v => v.Customer)
                  .WithMany(c => c.Vehicles)
                  .HasForeignKey(v => v.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

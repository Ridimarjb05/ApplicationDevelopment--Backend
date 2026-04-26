using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleParts.Domain.Entities;
using VehicleParts.Infrastructure.Identity;

namespace VehicleParts.Infrastructure.Data;


/// Main EF Core database context.
/// Extends IdentityDbContext to include ASP.NET Core Identity tables.

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Part>             Parts             => Set<Part>();
    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Part configuration 
        builder.Entity<Part>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).IsRequired().HasMaxLength(200);
            e.Property(p => p.SKU).IsRequired().HasMaxLength(100);
            e.HasIndex(p => p.SKU).IsUnique();                   // enforce unique SKU at DB level
            e.Property(p => p.Category).IsRequired().HasMaxLength(100);
            e.Property(p => p.Brand).HasMaxLength(100);
            e.Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
            e.HasQueryFilter(p => !p.IsDeleted);                 // global soft-delete filter
        });

        // StockTransaction configuration 
        builder.Entity<StockTransaction>(e =>
        {
            e.HasKey(st => st.Id);
            e.HasOne(st => st.Part)
             .WithMany(p => p.StockTransactions)
             .HasForeignKey(st => st.PartId)
             .OnDelete(DeleteBehavior.Cascade);
            e.Property(st => st.Reason).HasMaxLength(200);
            // Match the Part global filter so EF doesn't warn about filtered navigations
            e.HasQueryFilter(st => !st.Part.IsDeleted);
        });
    }
}

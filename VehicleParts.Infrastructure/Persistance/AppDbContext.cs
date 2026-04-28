using Microsoft.EntityFrameworkCore;
using VehicleParts.Domain.Models;

namespace VehicleParts.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<StaffDetail> StaffDetails { get; set; } = null!;
    public DbSet<CustomerDetail> CustomerDetails { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<Vendor> Vendors { get; set; } = null!;
    public DbSet<Part> Parts { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
    public DbSet<LoyaltyTransaction> LoyaltyTransactions { get; set; } = null!;
    public DbSet<PartRequest> PartRequests { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<ServiceReview> ServiceReviews { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<StockTransaction> StockTransactions { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // tell EF Core which property is the primary key for each table
        // this is needed because our ID names don't all follow the default EF naming rules
        builder.Entity<User>().HasKey(u => u.UserID);
        builder.Entity<StaffDetail>().HasKey(s => s.StaffID);
        builder.Entity<CustomerDetail>().HasKey(c => c.CustomerID);
        builder.Entity<Vehicle>().HasKey(v => v.VehicleID);
        builder.Entity<Vendor>().HasKey(v => v.VendorID);
        builder.Entity<Part>().HasKey(p => p.PartID);
        builder.Entity<Invoice>().HasKey(i => i.InvoiceID);
        builder.Entity<InvoiceItem>().HasKey(i => i.ID);
        builder.Entity<LoyaltyTransaction>().HasKey(l => l.ID);
        builder.Entity<PartRequest>().HasKey(p => p.ID);
        builder.Entity<Appointment>().HasKey(a => a.AppointmentID);
        builder.Entity<ServiceReview>().HasKey(s => s.ReviewID);
        builder.Entity<Notification>().HasKey(n => n.NotificationID);
        builder.Entity<StockTransaction>().HasKey(s => s.ID);

        // set decimal precision for all money/price columns
        builder.Entity<InvoiceItem>().Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
        builder.Entity<InvoiceItem>().Property(i => i.LineTotal).HasColumnType("decimal(18,2)");
        builder.Entity<Invoice>().Property(i => i.SubTotal).HasColumnType("decimal(18,2)");
        builder.Entity<Invoice>().Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Entity<Invoice>().Property(i => i.DiscountAmount).HasColumnType("decimal(18,2)");
        builder.Entity<Invoice>().Property(i => i.DiscountPercent).HasColumnType("decimal(18,2)");
        builder.Entity<Part>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Entity<CustomerDetail>().Property(c => c.TotalSpent).HasColumnType("decimal(18,2)");
        builder.Entity<CustomerDetail>().Property(c => c.CreditBalance).HasColumnType("decimal(18,2)");
    }

}

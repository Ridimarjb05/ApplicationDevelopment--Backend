using Microsoft.EntityFrameworkCore;
using VehicleParts.Domain.Models;

namespace VehicleParts.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<StaffDetail> StaffDetails => Set<StaffDetail>();
    public DbSet<CustomerDetail> CustomerDetails => Set<CustomerDetail>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<LoyaltyTransaction> LoyaltyTransactions => Set<LoyaltyTransaction>();
    public DbSet<PartRequest> PartRequests => Set<PartRequest>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<ServiceReview> ServiceReviews => Set<ServiceReview>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.UserID);
        modelBuilder.Entity<StaffDetail>().HasKey(s => s.StaffID);
        modelBuilder.Entity<CustomerDetail>().HasKey(c => c.CustomerID);
        modelBuilder.Entity<Vehicle>().HasKey(v => v.VehicleID);
        modelBuilder.Entity<Vendor>().HasKey(v => v.VendorID);
        modelBuilder.Entity<Part>().HasKey(p => p.PartID);
        modelBuilder.Entity<Invoice>().HasKey(i => i.InvoiceID);
        modelBuilder.Entity<InvoiceItem>().HasKey(i => i.ID);
        modelBuilder.Entity<LoyaltyTransaction>().HasKey(l => l.ID);
        modelBuilder.Entity<PartRequest>().HasKey(p => p.ID);
        modelBuilder.Entity<Appointment>().HasKey(a => a.AppointmentID);
        modelBuilder.Entity<ServiceReview>().HasKey(s => s.ReviewID);
        modelBuilder.Entity<Notification>().HasKey(n => n.NotificationID);
    }
}
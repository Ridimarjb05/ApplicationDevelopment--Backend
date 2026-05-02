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

        // Configure foreign key relationships
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.CustomerDetail)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.CustomerID);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.CustomerDetail)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CustomerID);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.StaffDetail)
            .WithMany(s => s.Invoices)
            .HasForeignKey(i => i.StaffID);

        modelBuilder.Entity<LoyaltyTransaction>()
            .HasOne(l => l.CustomerDetail)
            .WithMany(c => c.LoyaltyTransactions)
            .HasForeignKey(l => l.CustomerID);

        modelBuilder.Entity<PartRequest>()
            .HasOne(p => p.CustomerDetail)
            .WithMany(c => c.PartRequests)
            .HasForeignKey(p => p.CustomerID);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.CustomerDetail)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.CustomerID);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.StaffDetail)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.StaffID);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Vehicle)
            .WithMany()
            .HasForeignKey(a => a.VehicleID);

        modelBuilder.Entity<ServiceReview>()
            .HasOne(s => s.CustomerDetail)
            .WithMany(c => c.ServiceReviews)
            .HasForeignKey(s => s.CustomerID);

        modelBuilder.Entity<ServiceReview>()
            .HasOne(s => s.Appointment)
            .WithMany(a => a.ServiceReviews)
            .HasForeignKey(s => s.AppointmentID);

        modelBuilder.Entity<StaffDetail>()
            .HasOne(s => s.User)
            .WithOne(u => u.StaffDetail)
            .HasForeignKey<StaffDetail>(s => s.UserID);

        modelBuilder.Entity<CustomerDetail>()
            .HasOne(c => c.User)
            .WithOne(u => u.CustomerDetail)
            .HasForeignKey<CustomerDetail>(c => c.UserID);

        modelBuilder.Entity<Part>()
            .HasOne(p => p.Vendor)
            .WithMany(v => v.Parts)
            .HasForeignKey(p => p.VendorID);

        modelBuilder.Entity<InvoiceItem>()
            .HasOne(i => i.Invoice)
            .WithMany(inv => inv.InvoiceItems)
            .HasForeignKey(i => i.InvoiceID);

        modelBuilder.Entity<InvoiceItem>()
            .HasOne(i => i.Part)
            .WithMany(p => p.InvoiceItems)
            .HasForeignKey(i => i.PartID);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserID);
    }
}
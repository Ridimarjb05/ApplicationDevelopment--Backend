using Microsoft.EntityFrameworkCore;
using VehicleParts.Application.Interface.IRepository;
using VehicleParts.Domain.Models;
using VehicleParts.Infrastructure.Persistance;

namespace VehicleParts.Infrastructure.Repository;

public class VendorRepository : IVendorRepository
{
    private readonly AppDbContext _context;

    public VendorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vendor>> GetAllAsync()
    {
        return await _context.Vendors.AsNoTracking().ToListAsync();
    }

    public async Task<Vendor?> GetByIdAsync(int id)
    {
        return await _context.Vendors.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Vendor> CreateAsync(Vendor vendor)
    {
        vendor.CreatedAt = DateTime.UtcNow;
        vendor.UpdatedAt = DateTime.UtcNow;
        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();
        return vendor;
    }

    public async Task<Vendor?> UpdateAsync(int id, Vendor vendor)
    {
        var existing = await _context.Vendors.FindAsync(id);
        if (existing is null) return null;

        existing.Name = vendor.Name;
        existing.Email = vendor.Email;
        existing.Phone = vendor.Phone;
        existing.Address = vendor.Address;
        existing.ContactPerson = vendor.ContactPerson;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor is null) return false;

        _context.Vendors.Remove(vendor);
        await _context.SaveChangesAsync();
        return true;
    }
}

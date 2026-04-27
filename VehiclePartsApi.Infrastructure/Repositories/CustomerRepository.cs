using Microsoft.EntityFrameworkCore;
using VehiclePartsApi.Domain.Entities;
using VehiclePartsApi.Domain.Interfaces;
using VehiclePartsApi.Infrastructure.Data;

namespace VehiclePartsApi.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _db;
    public CustomerRepository(ApplicationDbContext db) => _db = db;

    public Task<CustomerProfile?> GetByIdAsync(int id) =>
        _db.CustomerProfiles.FirstOrDefaultAsync(c => c.Id == id);

    public Task<CustomerProfile?> GetByIdWithVehiclesAsync(int id) =>
        _db.CustomerProfiles.Include(c => c.Vehicles).FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(CustomerProfile profile)
    {
        _db.CustomerProfiles.Add(profile);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(CustomerProfile profile)
    {
        _db.CustomerProfiles.Update(profile);
        await _db.SaveChangesAsync();
    }

    public async Task<List<CustomerProfile>> SearchAsync(string type, string value, int take = 30)
    {
        value = value?.Trim() ?? "";
        
        var q = _db.CustomerProfiles.Include(c => c.Vehicles).AsQueryable();

        if (!string.IsNullOrWhiteSpace(value))
        {
            switch (type.Trim().ToLower())
            {
                case "name":
                    q = q.Where(c => c.FullName.ToLower().Contains(value.ToLower()));
                    break;
                case "phone":
                    q = q.Where(c => c.Phone.Contains(value));
                    break;
                case "id":
                    q = q.Where(c => c.Id.ToString() == value);
                    break;
                case "vehiclenumber":
                    q = q.Where(c => c.Vehicles.Any(v => v.VehicleNumber.ToLower().Contains(value.ToLower())));
                    break;
                case "email":
                    q = q.Where(c => c.Email.ToLower().Contains(value.ToLower()));
                    break;
            }
        }

        if (type.Trim().ToLower() == "recent")
        {
            q = q.OrderByDescending(c => c.CreatedAt);
        }

        return await q.Take(take).ToListAsync();
    }

    public Task<int> GetTotalCountAsync() => _db.CustomerProfiles.CountAsync();
}
using Microsoft.EntityFrameworkCore;
using VehiclePartsApi.Domain.Entities;
using VehiclePartsApi.Domain.Interfaces;
using VehiclePartsApi.Infrastructure.Data;

namespace VehiclePartsApi.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _db;
    public VehicleRepository(ApplicationDbContext db) => _db = db;

    public Task<Vehicle?> GetByIdAsync(int id) =>
        _db.Vehicles.FirstOrDefaultAsync(v => v.Id == id);

    public async Task AddAsync(Vehicle vehicle)
    {
        _db.Vehicles.Add(vehicle);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _db.Vehicles.Update(vehicle);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Vehicle vehicle)
    {
        _db.Vehicles.Remove(vehicle);
        await _db.SaveChangesAsync();
    }

    public Task<int> GetTotalCountAsync() => _db.Vehicles.CountAsync();
}
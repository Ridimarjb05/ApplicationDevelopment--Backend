using VehicleParts.Domain.Models;

namespace VehicleParts.Application.Interface.IRepository;

public interface IVendorRepository
{
    Task<IEnumerable<Vendor>> GetAllAsync();
    Task<Vendor?> GetByIdAsync(int id);
    Task<Vendor> CreateAsync(Vendor vendor);
    Task<Vendor?> UpdateAsync(int id, Vendor vendor);
    Task<bool> DeleteAsync(int id);
}

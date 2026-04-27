using VehiclePartsApi.Domain.Entities;

namespace VehiclePartsApi.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerProfile?> GetByIdAsync(int id);
    Task<CustomerProfile?> GetByIdWithVehiclesAsync(int id);
    Task AddAsync(CustomerProfile profile);
    Task UpdateAsync(CustomerProfile profile);
    Task<List<CustomerProfile>> SearchAsync(string type, string value, int take = 30);
    Task<int> GetTotalCountAsync();
}
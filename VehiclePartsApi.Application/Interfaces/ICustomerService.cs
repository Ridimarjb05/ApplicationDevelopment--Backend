using VehiclePartsApi.Application.DTOs.Customer;
using VehiclePartsApi.Domain.Entities;

namespace VehiclePartsApi.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerProfile?> GetCustomerAsync(int profileId);
    Task UpdateProfileAsync(int profileId, UpdateProfileDto dto);
    Task<int> AddVehicleAsync(int profileId, VehicleDto dto);
    Task UpdateVehicleAsync(int profileId, int vehicleId, VehicleDto dto);
    Task DeleteVehicleAsync(int profileId, int vehicleId);
}
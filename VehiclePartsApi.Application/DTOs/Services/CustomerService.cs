using VehiclePartsApi.Application.DTOs.Customer;
using VehiclePartsApi.Application.Interfaces;
using VehiclePartsApi.Domain.Entities;
using VehiclePartsApi.Domain.Interfaces;

namespace VehiclePartsApi.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepo;
    private readonly IVehicleRepository _vehicleRepo;

    public CustomerService(ICustomerRepository customerRepo, IVehicleRepository vehicleRepo)
    {
        _customerRepo = customerRepo;
        _vehicleRepo = vehicleRepo;
    }

    public Task<CustomerProfile?> GetCustomerAsync(int profileId) =>
        _customerRepo.GetByIdWithVehiclesAsync(profileId);

    public async Task UpdateProfileAsync(int profileId, UpdateProfileDto dto)
    {
        var profile = await _customerRepo.GetByIdAsync(profileId);
        if (profile == null) throw new Exception("Customer not found");

        profile.FullName = dto.FullName.Trim();
        profile.Phone = dto.Phone.Trim();
        await _customerRepo.UpdateAsync(profile);
    }

    public async Task<int> AddVehicleAsync(int profileId, VehicleDto dto)
    {
        var profile = await _customerRepo.GetByIdAsync(profileId);
        if (profile == null) throw new Exception("Customer not found");

        var v = new Vehicle
        {
            CustomerProfileId = profileId,
            VehicleNumber = dto.VehicleNumber.Trim(),
            Make = dto.Make.Trim(),
            Model = dto.Model.Trim(),
            Year = dto.Year
        };

        await _vehicleRepo.AddAsync(v);
        return v.Id;
    }

    public async Task UpdateVehicleAsync(int profileId, int vehicleId, VehicleDto dto)
    {
        var v = await _vehicleRepo.GetByIdAsync(vehicleId);
        if (v == null || v.CustomerProfileId != profileId)
            throw new Exception("Vehicle not found");

        v.VehicleNumber = dto.VehicleNumber.Trim();
        v.Make = dto.Make.Trim();
        v.Model = dto.Model.Trim();
        v.Year = dto.Year;
        await _vehicleRepo.UpdateAsync(v);
    }

    public async Task DeleteVehicleAsync(int profileId, int vehicleId)
    {
        var v = await _vehicleRepo.GetByIdAsync(vehicleId);
        if (v == null || v.CustomerProfileId != profileId)
            throw new Exception("Vehicle not found");

        await _vehicleRepo.DeleteAsync(v);
    }
}
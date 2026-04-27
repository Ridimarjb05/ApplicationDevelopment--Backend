using VehiclePartsApi.Application.DTOs.Auth;
using VehiclePartsApi.Application.Interfaces;
using VehiclePartsApi.Domain.Entities;
using VehiclePartsApi.Domain.Interfaces;

namespace VehiclePartsApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly ICustomerRepository _customerRepo;
    private readonly IVehicleRepository _vehicleRepo;

    public AuthService(ICustomerRepository customerRepo, IVehicleRepository vehicleRepo)
    {
        _customerRepo = customerRepo;
        _vehicleRepo = vehicleRepo;
    }

    public async Task<int> RegisterCustomerAsync(RegisterCustomerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FullName)) throw new Exception("FullName required");
        if (string.IsNullOrWhiteSpace(dto.Phone)) throw new Exception("Phone required");
        if (string.IsNullOrWhiteSpace(dto.Email)) throw new Exception("Email required");

        var profile = new CustomerProfile
        {
            FullName = dto.FullName.Trim(),
            Phone = dto.Phone.Trim(),
            Email = dto.Email.Trim()
        };

        await _customerRepo.AddAsync(profile);

        if (!string.IsNullOrWhiteSpace(dto.VehicleNumber))
        {
            await _vehicleRepo.AddAsync(new Vehicle
            {
                CustomerProfileId = profile.Id,
                VehicleNumber = dto.VehicleNumber.Trim(),
                Make = dto.Make?.Trim() ?? "",
                Model = dto.Model?.Trim() ?? "",
                Year = dto.Year ?? DateTime.UtcNow.Year
            });
        }

        return profile.Id;
    }
}
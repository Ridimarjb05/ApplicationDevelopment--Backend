using VehiclePartsApi.Application.DTOs.Auth;

namespace VehiclePartsApi.Application.Interfaces;

public interface IAuthService
{
    Task<int> RegisterCustomerAsync(RegisterCustomerDto dto);
}
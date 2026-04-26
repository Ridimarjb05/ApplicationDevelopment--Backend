using VehicleParts.Application.Common;
using VehicleParts.Application.DTOs;

namespace VehicleParts.Application.Interfaces;


/// Handles authentication — generating JWT tokens for valid credentials.

public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto dto);
    Task<ApiResponse<string>> RegisterCustomerAsync(RegisterCustomerRequestDto dto);
}

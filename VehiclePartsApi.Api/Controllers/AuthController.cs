using Microsoft.AspNetCore.Mvc;
using VehiclePartsApi.Application.DTOs.Auth;
using VehiclePartsApi.Application.Interfaces;

namespace VehiclePartsApi.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer(RegisterCustomerDto dto)
    {
        var id = await _authService.RegisterCustomerAsync(dto);
        return Ok(new { message = "Customer registered", profileId = id });
    }
}
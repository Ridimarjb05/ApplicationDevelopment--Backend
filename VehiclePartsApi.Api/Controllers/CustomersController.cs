using Microsoft.AspNetCore.Mvc;
using VehiclePartsApi.Application.DTOs.Customer;
using VehiclePartsApi.Application.Interfaces;

namespace VehiclePartsApi.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService) => _customerService = customerService;

    [HttpGet("{profileId:int}")]
    public async Task<IActionResult> GetCustomer(int profileId)
    {
        var c = await _customerService.GetCustomerAsync(profileId);
        return c == null ? NotFound("Customer not found") : Ok(c);
    }

    [HttpPut("{profileId:int}")]
    public async Task<IActionResult> UpdateProfile(int profileId, UpdateProfileDto dto)
    {
        await _customerService.UpdateProfileAsync(profileId, dto);
        return Ok(new { message = "Profile updated" });
    }

    [HttpPost("{profileId:int}/vehicles")]
    public async Task<IActionResult> AddVehicle(int profileId, VehicleDto dto)
    {
        var id = await _customerService.AddVehicleAsync(profileId, dto);
        return Ok(new { message = "Vehicle added", vehicleId = id });
    }

    [HttpPut("{profileId:int}/vehicles/{vehicleId:int}")]
    public async Task<IActionResult> UpdateVehicle(int profileId, int vehicleId, VehicleDto dto)
    {
        await _customerService.UpdateVehicleAsync(profileId, vehicleId, dto);
        return Ok(new { message = "Vehicle updated" });
    }

    [HttpDelete("{profileId:int}/vehicles/{vehicleId:int}")]
    public async Task<IActionResult> DeleteVehicle(int profileId, int vehicleId)
    {
        await _customerService.DeleteVehicleAsync(profileId, vehicleId);
        return Ok(new { message = "Vehicle deleted" });
    }
}
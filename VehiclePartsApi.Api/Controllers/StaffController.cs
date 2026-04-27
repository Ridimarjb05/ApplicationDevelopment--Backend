using Microsoft.AspNetCore.Mvc;
using VehiclePartsApi.Application.Interfaces;

namespace VehiclePartsApi.Api.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService) => _staffService = staffService;

    [HttpGet("customers/search")]
    public async Task<IActionResult> Search([FromQuery] string type, [FromQuery] string value)
    {
        var results = await _staffService.SearchCustomersAsync(type, value);
        return Ok(results);
    }

    [HttpGet("customers/recent")]
    public async Task<IActionResult> GetRecent([FromQuery] int count = 5)
    {
        var results = await _staffService.GetRecentRegistrationsAsync(count);
        return Ok(results);
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await _staffService.GetDashboardStatsAsync();
        return Ok(stats);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleParts.Application.DTOs.Staff;
using VehicleParts.Application.Interfaces;
using VehicleParts.Domain.Enums;

namespace VehicleParts.WebApi.Controllers.Admin;

// this controller handles adding and managing staff members
// only admins are allowed to use these endpoints
[ApiController]
[Route("api/admin/staff")]
[Authorize(Roles = AppRoles.Admin)]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    //  POST /api/admin/staff 

    // add a new staff member to the system
    // if we don't give a password, it will create a random one for them
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _staffService.CreateStaffAsync(dto);

        if (!result.Success)
        {
            // 409 if email already exists
            if (result.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                return Conflict(result);

            return BadRequest(result);
        }

        return StatusCode(StatusCodes.Status201Created, result);
    }

    // GET /api/admin/staff 

    // get the list of all staff members
    // we can also search and filter by role here
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStaff(
        [FromQuery] string? search   = null,
        [FromQuery] string? role     = null,
        [FromQuery] int     page     = 1,
        [FromQuery] int     pageSize = 10)
    {
        var result = await _staffService.GetAllStaffAsync(search, role, page, pageSize);
        return Ok(result);
    }

    // PUT /api/admin/staff/{id}/role

    // change someone's role (like promoting them to admin)
    [HttpPut("{id}/role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _staffService.UpdateRoleAsync(id, dto);

        if (!result.Success)
        {
            if (result.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);

            return BadRequest(result);
        }

        return Ok(result);
    }

    // PUT /api/admin/staff/{id}/status

    // turn a staff account on or off
    // inactive staff won't be able to log in anymore
    [HttpPut("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateStatusDto dto)
    {
        var result = await _staffService.UpdateStatusAsync(id, dto);

        if (!result.Success)
        {
            if (result.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);

            return BadRequest(result);
        }

        return Ok(result);
    }
}

using Microsoft.AspNetCore.Mvc;
using VehicleParts.Application.DTOs.Vendor;
using VehicleParts.Application.Interface.IServices;

namespace VehicleParts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorController : ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    /// <summary>GET /api/vendor — Retrieve all vendors</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vendors = await _vendorService.GetAllVendorsAsync();
        return Ok(vendors);
    }

    /// <summary>GET /api/vendor/{id} — Retrieve a vendor by ID</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vendor = await _vendorService.GetVendorByIdAsync(id);
        if (vendor is null)
            return NotFound(new { message = $"Vendor with ID {id} not found." });

        return Ok(vendor);
    }

    /// <summary>POST /api/vendor — Create a new vendor</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVendorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _vendorService.CreateVendorAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>PUT /api/vendor/{id} — Update an existing vendor</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVendorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _vendorService.UpdateVendorAsync(id, dto);
        if (updated is null)
            return NotFound(new { message = $"Vendor with ID {id} not found." });

        return Ok(updated);
    }

    /// <summary>DELETE /api/vendor/{id} — Delete a vendor</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _vendorService.DeleteVendorAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Vendor with ID {id} not found." });

        return NoContent();
    }
}

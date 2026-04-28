using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleParts.Domain.Models;
using VehicleParts.Infrastructure.Persistance;

namespace VehicleParts.API.Controllers.Admin;

// this controller is used to manage vendors (the companies that supply parts)
// only admin users can use these endpoints
[ApiController]
[Route("api/admin/vendors")]
[Authorize(Roles = "Admin")]
public class VendorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public VendorsController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/admin/vendors - get all vendors
    [HttpGet]
    public IActionResult GetAll()
    {
        var vendors = _context.Vendors.Where(v => v.IsActive).ToList();
        return Ok(vendors);
    }

    // POST /api/admin/vendors - add a new vendor
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVendorRequest dto)
    {
        var vendor = new Vendor
        {
            Name = dto.Name,
            ContactPerson = dto.ContactPerson,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();
        return Ok(vendor);
    }
}

// simple request model for creating a vendor
public class CreateVendorRequest
{
    public string Name { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

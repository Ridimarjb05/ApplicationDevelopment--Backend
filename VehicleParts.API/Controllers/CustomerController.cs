using Microsoft.AspNetCore.Mvc;
using VehicleParts.Application.DTOs.Customer;
using VehicleParts.Application.Interface.IServices;

namespace VehicleParts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>GET /api/customer — Retrieve all customers with their vehicles</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    /// <summary>GET /api/customer/{id} — Retrieve a customer by ID with vehicle details</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer is null)
            return NotFound(new { message = $"Customer with ID {id} not found." });

        return Ok(customer);
    }

    /// <summary>POST /api/customer — Register a new customer with vehicle details (Staff)</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _customerService.CreateCustomerAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>PUT /api/customer/{id} — Update customer profile information</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _customerService.UpdateCustomerAsync(id, dto);
        if (updated is null)
            return NotFound(new { message = $"Customer with ID {id} not found." });

        return Ok(updated);
    }

    /// <summary>DELETE /api/customer/{id} — Delete a customer record</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _customerService.DeleteCustomerAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Customer with ID {id} not found." });

        return NoContent();
    }
}

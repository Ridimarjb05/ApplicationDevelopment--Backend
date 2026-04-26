using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleParts.Application.DTOs.Parts;
using VehicleParts.Application.Interfaces;
using VehicleParts.Domain.Enums;

namespace VehicleParts.WebApi.Controllers.Admin;

// this controller handles adding, editing, and deleting parts in the inventory
// only admins are allowed to use these endpoints
[ApiController]
[Route("api/admin/parts")]
[Authorize(Roles = AppRoles.Admin)]
public class PartsController : ControllerBase
{
    private readonly IPartService _partService;

    public PartsController(IPartService partService)
    {
        _partService = partService;
    }

    // POST /api/admin/parts 

    // add a brand new part to our inventory database
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreatePart([FromBody] CreatePartDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _partService.CreatePartAsync(dto);

        if (!result.Success)
        {
            if (result.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                return Conflict(result);

            return BadRequest(result);
        }

        return StatusCode(StatusCodes.Status201Created, result);
    }

    // ── GET /api/admin/parts

    // get all the parts so we can show them in the table
    // we also handle searching and sorting here
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllParts(
        [FromQuery] string? search   = null,
        [FromQuery] string? sortBy   = null,
        [FromQuery] int     page     = 1,
        [FromQuery] int     pageSize = 10)
    {
        var query  = new PartQueryDto { Search = search, SortBy = sortBy, Page = page, PageSize = pageSize };
        var result = await _partService.GetAllPartsAsync(query);
        return Ok(result);
    }

    // GET /api/admin/parts/{id}

    // get just one specific part by its id
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPart(Guid id)
    {
        var result = await _partService.GetPartByIdAsync(id);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    //  PUT /api/admin/parts/{id} 

    // update the details of a part (like price or name)
    // we don't let them change the SKU though
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePart(Guid id, [FromBody] UpdatePartDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _partService.UpdatePartAsync(id, dto);

        if (!result.Success)
        {
            if (result.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);

            return BadRequest(result);
        }

        return Ok(result);
    }

    //  DELETE /api/admin/parts/{id} 

    // delete a part from the system
    // we actually just hide it so we don't lose the old sales data
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePart(Guid id)
    {
        var result = await _partService.DeletePartAsync(id);

        if (!result.Success)
        {
            if (result.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);

            return BadRequest(result);
        }

        return Ok(result);
    }

    //  POST /api/admin/parts/{id}/stock-in 

    // add more stock to a part when we buy more from a vendor
    [HttpPost("{id:guid}/stock-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> StockIn(Guid id, [FromBody] StockInDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _partService.StockInAsync(id, dto);

        if (!result.Success)
        {
            if (result.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);

            return BadRequest(result);
        }

        return Ok(result);
    }
}

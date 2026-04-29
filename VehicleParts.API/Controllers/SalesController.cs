using Microsoft.AspNetCore.Mvc;
using VehicleParts.Application.DTOs;
using VehicleParts.Application.Interface.IServices;

namespace VehicleParts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost("create-sale")]
        public IActionResult CreateSale([FromBody] SaleRequestDto request)
        {
            var sale = _salesService.CreateSale(request);
            return Ok(sale);
        }
    }
}
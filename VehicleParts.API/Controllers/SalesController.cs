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

        // Feature 7: Create sales invoice
        [HttpPost("create-sale")]
        public IActionResult CreateSale([FromBody] SaleRequestDto request)
        {
            var sale = _salesService.CreateSale(request);
            return Ok(sale);
        }

        // Feature 16: Loyalty discount calculation
        [HttpPost("calculate-loyalty-discount")]
        public IActionResult CalculateLoyaltyDiscount([FromBody] SaleRequestDto request)
        {
            var sale = _salesService.CalculateLoyaltyDiscount(request);

            return Ok(new
            {
                sale.TotalAmount,
                sale.Discount,
                sale.FinalAmount,
                Message = sale.Discount > 0
                    ? "10% loyalty discount applied."
                    : "No loyalty discount applied."
            });
        }
    }
}
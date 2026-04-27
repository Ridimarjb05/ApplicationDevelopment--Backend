using Microsoft.AspNetCore.Mvc;
using VehicleParts.Domain.Models;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private static List<Sale> sales = new List<Sale>();

    [HttpPost("create-sale")]
    public IActionResult CreateSale([FromBody] SaleRequest request)
    {
        decimal total = request.Items.Sum(i => i.Price * i.Quantity);

        decimal discount = 0;

        // 🔥 Feature 16: Loyalty logic
        if (total > 5000)
        {
            discount = total * 0.10m;
        }

        decimal finalAmount = total - discount;

        var sale = new Sale
        {
            Id = sales.Count + 1,
            CustomerId = request.CustomerId,
            TotalAmount = total,
            Discount = discount,
            FinalAmount = finalAmount,
            Date = DateTime.Now
        };

        sales.Add(sale);

        return Ok(sale);
    }
}
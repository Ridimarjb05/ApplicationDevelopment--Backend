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

        // Feature 16: Apply 10% loyalty discount when total purchase amount is greater than 5000
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
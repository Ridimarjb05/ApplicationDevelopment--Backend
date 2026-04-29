using VehicleParts.Application.DTOs;
using VehicleParts.Application.Interface.IServices;
using VehicleParts.Domain.Models;

namespace VehicleParts.Application.Services
{
    public class SalesService : ISalesService
    {
        private static List<Sale> sales = new();

        public Sale CreateSale(SaleRequestDto request)
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

            return sale;
        }
    }
}
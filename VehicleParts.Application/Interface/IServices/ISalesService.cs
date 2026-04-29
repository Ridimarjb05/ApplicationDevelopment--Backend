using VehicleParts.Application.DTOs;
using VehicleParts.Domain.Models;

namespace VehicleParts.Application.Interface.IServices
{
    public interface ISalesService
    {
        Sale CreateSale(SaleRequestDto request);
        Sale CalculateLoyaltyDiscount(SaleRequestDto request);
    }
}
using VehicleParts.Application.Common;
using VehicleParts.Application.DTOs.Parts;
using VehicleParts.Domain.Entities;

namespace VehicleParts.Application.Interfaces;


/// Data-access contract for the Parts entity — used by PartService.
/// Keeps EF Core out of the Application layer

public interface IPartRepository
{
    Task<Part?> GetByIdAsync(Guid id);
    Task<Part?> GetBySkuAsync(string sku);
    Task<(IEnumerable<Part> Items, int TotalCount)> GetPagedAsync(PartQueryDto query);
    Task<Part> AddAsync(Part part);
    Task UpdateAsync(Part part);
    Task AddStockTransactionAsync(StockTransaction transaction);
    Task SaveChangesAsync();
}

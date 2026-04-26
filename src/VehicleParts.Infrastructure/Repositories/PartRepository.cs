using Microsoft.EntityFrameworkCore;
using VehicleParts.Application.DTOs.Parts;
using VehicleParts.Application.Interfaces;
using VehicleParts.Domain.Entities;
using VehicleParts.Infrastructure.Data;

namespace VehicleParts.Infrastructure.Repositories;


/// EF Core implementation of IPartRepository.
/// Handles all database operations for the Part entity.

public class PartRepository : IPartRepository
{
    private readonly AppDbContext _ctx;

    public PartRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Part?> GetByIdAsync(Guid id)
        // IgnoreQueryFilters allows finding soft-deleted records if needed; omit it to respect filter
        => await _ctx.Parts.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Part?> GetBySkuAsync(string sku)
        => await _ctx.Parts
                     .IgnoreQueryFilters()   // check even soft-deleted records for uniqueness
                     .FirstOrDefaultAsync(p => p.SKU == sku);

    public async Task<(IEnumerable<Part> Items, int TotalCount)> GetPagedAsync(PartQueryDto query)
    {
        // Base query — global filter already excludes IsDeleted=true
        var q = _ctx.Parts.AsQueryable();

        // Search by name, SKU, or category
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var s = query.Search.ToLower();
            q = q.Where(p => p.Name.ToLower().Contains(s)
                           || p.SKU.ToLower().Contains(s)
                           || p.Category.ToLower().Contains(s));
        }

        // Sort
        q = (query.SortBy?.ToLower()) switch
        {
            "price" => q.OrderBy(p => p.UnitPrice),
            "stock" => q.OrderBy(p => p.StockQty),
            _       => q.OrderBy(p => p.Name)        // default: by name
        };

        var totalCount = await q.CountAsync();

        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Part> AddAsync(Part part)
    {
        var entry = await _ctx.Parts.AddAsync(part);
        return entry.Entity;
    }

    public Task UpdateAsync(Part part)
    {
        _ctx.Parts.Update(part);
        return Task.CompletedTask;
    }

    public async Task AddStockTransactionAsync(StockTransaction transaction)
        => await _ctx.StockTransactions.AddAsync(transaction);

    public async Task SaveChangesAsync()
        => await _ctx.SaveChangesAsync();
}

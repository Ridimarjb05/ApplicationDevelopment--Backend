using AutoMapper;
using VehicleParts.Application.Common;
using VehicleParts.Application.DTOs.Parts;
using VehicleParts.Application.Interfaces;
using VehicleParts.Domain.Entities;

namespace VehicleParts.Application.Services;

// this service handles creating, editing, and deleting the actual car parts
// it talks to the database repository to save changes
public class PartService : IPartService
{
    private readonly IPartRepository _repo;
    private readonly IMapper         _mapper;

    public PartService(IPartRepository repo, IMapper mapper)
    {
        _repo   = repo;
        _mapper = mapper;
    }

    // POST /api/admin/parts 

    public async Task<ApiResponse<PartResponseDto>> CreatePartAsync(CreatePartDto dto)
    {
        // Validate
        var errors = ValidateCreateDto(dto);
        if (errors.Any())
            return ApiResponse<PartResponseDto>.Fail("Validation failed.", errors);

        // Enforce unique SKU
        if (await _repo.GetBySkuAsync(dto.SKU) is not null)
            return ApiResponse<PartResponseDto>.Fail($"A part with SKU '{dto.SKU}' already exists.");

        var part = _mapper.Map<Part>(dto);
        part.CreatedAt = DateTime.UtcNow;
        part.UpdatedAt = DateTime.UtcNow;

        var created = await _repo.AddAsync(part);
        await _repo.SaveChangesAsync();

        return ApiResponse<PartResponseDto>.Ok(
            _mapper.Map<PartResponseDto>(created),
            "Part created successfully.");
    }

    // GET /api/admin/parts 

    public async Task<ApiResponse<PagedResult<PartResponseDto>>> GetAllPartsAsync(PartQueryDto query)
    {
        if (query.Page < 1)     query.Page     = 1;
        if (query.PageSize < 1) query.PageSize = 10;

        var (items, totalCount) = await _repo.GetPagedAsync(query);

        return ApiResponse<PagedResult<PartResponseDto>>.Ok(new PagedResult<PartResponseDto>
        {
            Items      = _mapper.Map<IEnumerable<PartResponseDto>>(items),
            TotalCount = totalCount,
            Page       = query.Page,
            PageSize   = query.PageSize
        });
    }

    // GET /api/admin/parts/{id} 

    public async Task<ApiResponse<PartResponseDto>> GetPartByIdAsync(Guid id)
    {
        var part = await _repo.GetByIdAsync(id);
        if (part is null)
            return ApiResponse<PartResponseDto>.Fail("Part not found.");

        return ApiResponse<PartResponseDto>.Ok(_mapper.Map<PartResponseDto>(part));
    }

    //PUT /api/admin/parts/{id} 

    public async Task<ApiResponse<PartResponseDto>> UpdatePartAsync(Guid id, UpdatePartDto dto)
    {
        var part = await _repo.GetByIdAsync(id);
        if (part is null)
            return ApiResponse<PartResponseDto>.Fail("Part not found.");

        // Apply only provided (non-null) fields
        if (dto.Name         is not null) part.Name         = dto.Name;
        if (dto.Category     is not null) part.Category     = dto.Category;
        if (dto.Brand        is not null) part.Brand        = dto.Brand;
        if (dto.Description  is not null) part.Description  = dto.Description;
        if (dto.UnitPrice    is not null)
        {
            if (dto.UnitPrice < 0)
                return ApiResponse<PartResponseDto>.Fail("UnitPrice cannot be negative.");
            part.UnitPrice = dto.UnitPrice.Value;
        }
        if (dto.ReorderLevel is not null)
        {
            if (dto.ReorderLevel < 0)
                return ApiResponse<PartResponseDto>.Fail("ReorderLevel cannot be negative.");
            part.ReorderLevel = dto.ReorderLevel.Value;
        }

        part.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(part);
        await _repo.SaveChangesAsync();

        return ApiResponse<PartResponseDto>.Ok(
            _mapper.Map<PartResponseDto>(part),
            "Part updated successfully.");
    }

    // DELETE /api/admin/parts/{id} 

    public async Task<ApiResponse> DeletePartAsync(Guid id)
    {
        var part = await _repo.GetByIdAsync(id);
        if (part is null)
            return ApiResponse.Fail("Part not found.");

        // Soft delete — preserves stock history
        part.IsDeleted = true;
        part.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(part);
        await _repo.SaveChangesAsync();

        return ApiResponse.Ok("Part deleted successfully.");
    }

    // POST /api/admin/parts/{id}/stock-in 

    public async Task<ApiResponse<PartResponseDto>> StockInAsync(Guid id, StockInDto dto)
    {
        if (dto.Quantity <= 0)
            return ApiResponse<PartResponseDto>.Fail("Quantity must be greater than zero.");

        var part = await _repo.GetByIdAsync(id);
        if (part is null)
            return ApiResponse<PartResponseDto>.Fail("Part not found.");

        // Increase stock
        part.StockQty  += dto.Quantity;
        part.UpdatedAt  = DateTime.UtcNow;

        await _repo.UpdateAsync(part);

        // Record stock transaction for audit trail
        var transaction = new StockTransaction
        {
            PartId    = part.Id,
            Quantity  = dto.Quantity,
            Reason    = string.IsNullOrWhiteSpace(dto.Reason) ? "Purchase" : dto.Reason,
            CreatedAt = DateTime.UtcNow
        };
        await _repo.AddStockTransactionAsync(transaction);
        await _repo.SaveChangesAsync();

        return ApiResponse<PartResponseDto>.Ok(
            _mapper.Map<PartResponseDto>(part),
            $"Stock increased by {dto.Quantity} unit(s). New total: {part.StockQty}.");
    }

    //  Validation helpers

    private static List<string> ValidateCreateDto(CreatePartDto dto)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(dto.Name))     errors.Add("Name is required.");
        if (string.IsNullOrWhiteSpace(dto.SKU))      errors.Add("SKU is required.");
        if (string.IsNullOrWhiteSpace(dto.Category)) errors.Add("Category is required.");
        if (dto.UnitPrice < 0)  errors.Add("UnitPrice cannot be negative.");
        if (dto.StockQty  < 0)  errors.Add("StockQty cannot be negative.");
        return errors;
    }
}

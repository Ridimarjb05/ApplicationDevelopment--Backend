using VehicleParts.Application.Common;
using VehicleParts.Application.DTOs.Parts;

namespace VehicleParts.Application.Interfaces;


/// Manages vehicle parts inventory — Feature 3.

public interface IPartService
{
    /// Creates a new part; rejects duplicate SKUs.
    Task<ApiResponse<PartResponseDto>> CreatePartAsync(CreatePartDto dto);

    ///Returns a paginated, optionally filtered and sorted list of active parts.

    /// Returns a single part by its Id.
    Task<ApiResponse<PartResponseDto>> GetPartByIdAsync(Guid id);

    /// Updates editable fields; SKU is immutable.
    Task<ApiResponse<PartResponseDto>> UpdatePartAsync(Guid id, UpdatePartDto dto);

    /// Soft-deletes a part (sets IsDeleted = true)
    Task<ApiResponse> DeletePartAsync(Guid id);

    /// Purchases stock — adds quantity and records a StockTransaction
    Task<ApiResponse<PartResponseDto>> StockInAsync(Guid id, StockInDto dto);
}

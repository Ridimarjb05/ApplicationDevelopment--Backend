using VehicleParts.Application.DTOs;

namespace VehicleParts.Application.Interface.IServices;

// this is the contract for Feature 3 - Parts inventory management
// the actual code is written in Infrastructure/Services/PartService.cs
public interface IPartService
{
    // get all active parts from the database
    Task<IEnumerable<PartResponseDto>> GetAllPartsAsync();

    // search parts by name or category using a keyword
    Task<IEnumerable<PartResponseDto>> SearchPartsAsync(string keyword);

    // get one part by its ID
    Task<PartResponseDto?> GetPartByIdAsync(int partId);

    // add a new part to the inventory (admin only)
    Task<PartResponseDto> CreatePartAsync(CreatePartDto dto);

    // update an existing part's details
    Task<PartResponseDto?> UpdatePartAsync(int partId, UpdatePartDto dto);

    // delete a part from the inventory
    Task<bool> DeletePartAsync(int partId);

    // add more stock to an existing part and record the transaction
    Task<PartResponseDto?> StockInAsync(int partId, StockInDto dto);
}

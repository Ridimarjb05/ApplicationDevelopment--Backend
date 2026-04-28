using VehicleParts.Application.DTOs.Vendor;

namespace VehicleParts.Application.Interface.IServices;

public interface IVendorService
{
    Task<IEnumerable<VendorResponseDto>> GetAllVendorsAsync();
    Task<VendorResponseDto?> GetVendorByIdAsync(int id);
    Task<VendorResponseDto> CreateVendorAsync(CreateVendorDto dto);
    Task<VendorResponseDto?> UpdateVendorAsync(int id, UpdateVendorDto dto);
    Task<bool> DeleteVendorAsync(int id);
}

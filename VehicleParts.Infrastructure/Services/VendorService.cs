using AutoMapper;
using VehicleParts.Application.DTOs.Vendor;
using VehicleParts.Application.Interface.IRepository;
using VehicleParts.Application.Interface.IServices;
using VehicleParts.Domain.Models;

namespace VehicleParts.Infrastructure.Services;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _repository;
    private readonly IMapper _mapper;

    public VendorService(IVendorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VendorResponseDto>> GetAllVendorsAsync()
    {
        var vendors = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<VendorResponseDto>>(vendors);
    }

    public async Task<VendorResponseDto?> GetVendorByIdAsync(int id)
    {
        var vendor = await _repository.GetByIdAsync(id);
        return vendor is null ? null : _mapper.Map<VendorResponseDto>(vendor);
    }

    public async Task<VendorResponseDto> CreateVendorAsync(CreateVendorDto dto)
    {
        var vendor = _mapper.Map<Vendor>(dto);
        var created = await _repository.CreateAsync(vendor);
        return _mapper.Map<VendorResponseDto>(created);
    }

    public async Task<VendorResponseDto?> UpdateVendorAsync(int id, UpdateVendorDto dto)
    {
        var vendor = _mapper.Map<Vendor>(dto);
        var updated = await _repository.UpdateAsync(id, vendor);
        return updated is null ? null : _mapper.Map<VendorResponseDto>(updated);
    }

    public async Task<bool> DeleteVendorAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}

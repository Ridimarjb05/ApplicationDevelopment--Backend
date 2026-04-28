using AutoMapper;
using VehicleParts.Application.DTOs.Customer;
using VehicleParts.Application.DTOs.Vehicle;
using VehicleParts.Application.DTOs.Vendor;
using VehicleParts.Domain.Models;

namespace VehicleParts.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Vendor mappings
        CreateMap<Vendor, VendorResponseDto>();
        CreateMap<CreateVendorDto, Vendor>();
        CreateMap<UpdateVendorDto, Vendor>();

        // Vehicle mappings
        CreateMap<Vehicle, VehicleResponseDto>();
        CreateMap<CreateVehicleDto, Vehicle>();

        // Customer mappings
        CreateMap<Customer, CustomerResponseDto>();
        CreateMap<CreateCustomerDto, Customer>()
            .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(src => src.Vehicles));
        CreateMap<UpdateCustomerDto, Customer>();
    }
}

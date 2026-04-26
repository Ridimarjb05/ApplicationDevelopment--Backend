using AutoMapper;
using VehicleParts.Application.DTOs.Parts;
using VehicleParts.Domain.Entities;

namespace VehicleParts.Application.Mappings;


/// AutoMapper profiles that map between domain entities and application DTOs.
/// Staff mappings are handled directly via UserManager — no AutoMapper needed there.

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Part → PartResponseDto
        CreateMap<Part, PartResponseDto>()
            .ForMember(dest => dest.IsLowStock,
                       opt  => opt.MapFrom(src => src.StockQty < src.ReorderLevel));

        // CreatePartDto → Part
        CreateMap<CreatePartDto, Part>()
            .ForMember(dest => dest.Id,        opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.StockTransactions, opt => opt.Ignore());
    }
}

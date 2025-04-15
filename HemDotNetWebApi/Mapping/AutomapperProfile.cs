using AutoMapper;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Mapping
{
    public class AutomapperProfile : Profile
    {

        public AutomapperProfile()
        {
            // Allan
            CreateMap<MarketProperty, MarketPropertyListingDto>()
            .ForMember(dest => dest.MunicipalityName, opt => opt.MapFrom(src => src.Municipality.MunicipalityName))
            .ForMember(dest => dest.PropertyImage, opt => opt.MapFrom(src =>
                src.Images != null && src.Images.Any() ? src.Images.First().PropertyImageUrl : null));
        }
    }
}

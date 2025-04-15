using AutoMapper;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Mapping
{
    public class AutomapperProfile : Profile
    {

        public AutomapperProfile()
        {
            //Author: Johan Ek
            CreateMap<MarketProperty, PartialMarketPropertyDTO>()
                .ForMember(dest => dest.Images,
                opt => opt.MapFrom(
                    src => src.Images));
            //Author: Johan Ek
            CreateMap<PropertyImage, PartialPropertyImageDTO>()
                .ForMember(dest => dest.PropertyImageUrl,
                opt => opt.MapFrom(
                    src => src.PropertyImageUrl));
        }
    }
}

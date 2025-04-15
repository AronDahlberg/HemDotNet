using AutoMapper;
using HemDotNetWebApi.DTOs;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            CreateMap<MarketProperty, ActiveMarketListingDTO>()
                .ForMember(dest => dest.MarketPropertyId, opt => opt.MapFrom(src => src.MarketPropertyId))
                .ForMember(dest => dest.PropertyAddress, opt => opt.MapFrom(src => src.PropertyAddress));
        }
    }
}

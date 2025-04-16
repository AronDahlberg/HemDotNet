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
                .ForMember(d => d.MunicipalityName, o => o.MapFrom(s => s.Municipality.MunicipalityName));
        }
    }
}

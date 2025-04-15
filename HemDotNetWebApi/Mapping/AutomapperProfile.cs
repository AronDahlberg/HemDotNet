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
            CreateMap<MarketProperty, PartialMarketPropertyDTO>();
        }
    }
}

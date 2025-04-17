using AutoMapper;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.DTOs;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // Allan
            CreateMap<MarketPropertyUpdateDto, MarketProperty>()
            .ForMember(dest => dest.Municipality, opt => opt.MapFrom(src =>
                new Municipality { MunicipalityId = src.MunicipalityId }))
            .ForMember(dest => dest.RealEstateAgent, opt => opt.MapFrom(src =>
                new RealEstateAgent { RealEstateAgentId = src.RealEstateAgentId }))
            .ForMember(dest => dest.Images, opt => opt.Ignore());

            //Author: Johan Ek
            CreateMap<MarketProperty, PartialMarketPropertyDTO>()
                .ForMember(dest => dest.Images,
               opt => opt.MapFrom(
                    src => src.Images))
                .ForMember(dest => dest.MunicipalityName,
                opt => opt.MapFrom(
                    src => src.Municipality.MunicipalityName));
            //Author: Johan Ek
            CreateMap<PropertyImage, PartialPropertyImageDTO>()
                .ForMember(dest => dest.PropertyImageUrl,
                opt => opt.MapFrom(
                    src => src.PropertyImageUrl));

            // Allan
            CreateMap<MarketProperty, MarketPropertyListingDto>()
            .ForMember(dest => dest.MunicipalityName, opt => opt.MapFrom(src => src.Municipality.MunicipalityName))
            .ForMember(dest => dest.PropertyImage, opt => opt.MapFrom(src =>
                src.Images != null && src.Images.Any() ? src.Images.First().PropertyImageUrl : null));

            // Christian
            CreateMap<MarketProperty, ActiveMarketListingDTO>()
                .ForMember(d => d.MunicipalityName, o => o.MapFrom(s => s.Municipality.MunicipalityName));

        }
    }
}

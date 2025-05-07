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
                new RealEstateAgent { Id = src.RealEstateAgentId }))
            .ForMember(dest => dest.Images, opt => opt.Ignore());

            // Allan
            CreateMap<MarketProperty, MarketPropertyDto>()
            .ForMember(dest => dest.MunicipalityId, opt =>
                opt.MapFrom(src => src.Municipality.MunicipalityId))
            .ForMember(dest => dest.RealEstateAgentId, opt =>
                opt.MapFrom(src => src.RealEstateAgent.Id));


            // Allan
            // PropertyImage mappings
            CreateMap<PropertyImage, PropertyImageDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.PropertyImageUrl))
                .ForMember(dest => dest.MarketPropertyId, opt =>
                    opt.MapFrom(src => src.MarketProperty.MarketPropertyId));

            CreateMap<PropertyImageDto, PropertyImage>()
                .ForMember(dest => dest.MarketProperty, opt =>
                    opt.MapFrom(src => new MarketProperty { MarketPropertyId = src.MarketPropertyId }));
            //

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

            // Christian
            CreateMap<RealEstateAgent, RealEstateAgentUpdateDTO>();


            // Katarina
            CreateMap<MarketProperty, MarketPropertyDetailsDto>()
    .ForMember(dest => dest.MunicipalityName, opt => opt.MapFrom(src => src.Municipality.MunicipalityName))
    .ForMember(dest => dest.RealEstateAgentFullName,
        opt => opt.MapFrom(src => $"{src.RealEstateAgent.RealEstateAgentLastName} {src.RealEstateAgent.RealEstateAgentFirstName}"))
    .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            // Christian
            CreateMap<RealEstateAgentUpdateDTO, RealEstateAgent>();

            // Katarina
            CreateMap<MarketProperty, MarketPropertyDetailsDto>()
    .ForMember(dest => dest.MunicipalityName, opt => opt.MapFrom(src => src.Municipality.MunicipalityName))
    .ForMember(dest => dest.RealEstateAgentFullName,
        opt => opt.MapFrom(src => $"{src.RealEstateAgent.RealEstateAgentFirstName} {src.RealEstateAgent.RealEstateAgentLastName}"))
    .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            // Katarina
            CreateMap<MarketPropertyCreateDto, MarketProperty>()
                        .ForMember(dest => dest.Municipality, opt => opt.MapFrom(src =>
                            new Municipality { MunicipalityId = src.MunicipalityId })) 
                        .ForMember(dest => dest.RealEstateAgent, opt => opt.MapFrom(src =>
                            new RealEstateAgent { Id = src.RealEstateAgentId }))  
                        .ForMember(dest => dest.Images, opt => opt.Ignore())  
                        .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))  
                        .ForMember(dest => dest.IsSold, opt => opt.MapFrom(src => false))  
                        .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow));


            // Allan
            CreateMap<Municipality, MunicipalityDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MunicipalityName));

            // Allan
            CreateMap<MarketProperty, PartialMarketPropertyDTO>()
            .ForMember(dest => dest.MunicipalityName, opt => opt.MapFrom(src => src.Municipality.MunicipalityName))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<PropertyImage, PartialPropertyImageDTO>();

            // Allan
            CreateMap<RealEstateAgent, RealEstateAgentDto>()
            .ForMember(dest => dest.RealEstateAgentAgencyName,
                opt => opt.MapFrom(src => src.RealEstateAgentAgency.RealEstateAgencyName))
            .ForMember(dest => dest.RealEstateAgentAgencyId,
                opt => opt.MapFrom(src => src.RealEstateAgentAgency.RealEstateAgencyId))
            .ForMember(dest => dest.RealEstateAgencyLogoUrl,
                opt => opt.MapFrom(src => src.RealEstateAgentAgency.RealEstateAgencyLogoUrl))
            .ForMember(dest => dest.RealEstateAgencyPresentation,
                opt => opt.MapFrom(src => src.RealEstateAgentAgency.RealEstateAgencyPresentation))
            .ForMember(dest => dest.PropertyIds,
                opt => opt.MapFrom(src => src.RealEstateAgentProperties.Select(p => p.MarketPropertyId)));
        }
    }
}

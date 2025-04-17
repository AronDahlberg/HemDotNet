using AutoMapper;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.DTOs;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HemDotNetWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class MarketPropertyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMarketPropertyRepository _marketPropertyRepository;

        public MarketPropertyController(IMapper mapper, IMarketPropertyRepository marketPropertyRepository)
        {
            _mapper = mapper;
            _marketPropertyRepository = marketPropertyRepository;
        }

        // Allan
        [HttpGet("byMunicipality/{municipality}")]
        public async Task<ActionResult<IEnumerable<MarketPropertyListingDto>>> GetMarketPropertyByMunicipality(string municipality)
        {
            var properties = await _marketPropertyRepository.GetAllByMunicipality(municipality);

            if (properties == null || !properties.Any())
            {
                return NotFound($"No properties were found for: {municipality}");
            }

            return Ok(_mapper.Map<IEnumerable<MarketPropertyListingDto>>(properties));
         }
        
        // Chris
        [HttpGet("ByAgent/{agentId}")]
        public async Task<IEnumerable<ActiveMarketListingDTO>> GetByAgent(int agentId)
        {
            var activeListings = await _marketPropertyRepository.GetAllActiveByAgent(agentId);
            var activeListingDtos = _mapper.Map<IEnumerable<ActiveMarketListingDTO>>(activeListings);
            return activeListingDtos;
        }
    }
}

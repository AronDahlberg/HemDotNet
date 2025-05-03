using AutoMapper;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.DTOs;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        [HttpPut]
        [Authorize]

        public async Task<ActionResult<MarketPropertyDto>> UpdateMarketProperty(MarketPropertyUpdateDto updateDto)
        {
            try
            {
                var marketPropertyToUpdate = _mapper.Map<MarketProperty>(updateDto);

                var updatedProperty = await _marketPropertyRepository.UpdateMarketProperty(marketPropertyToUpdate);

                if (updatedProperty == null)
                {
                    return NotFound($"Market property with ID {updateDto.MarketPropertyId} not found.");
                }

                var resultDto = _mapper.Map<MarketPropertyDto>(updatedProperty);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not update the market property. Please try again.");
            }
        }
        //Author: Johan Ek
        //[ProducesResponseType(typeof(List<PartialMarketPropertyDTO>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("MarketProperties/")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PartialMarketPropertyDTO>>> GetAllMarketPropertiesPartial()
        {
            var partialMarketPropertiesDTO = await _marketPropertyRepository.GetAllMarketPropertiesPartial();
            if (partialMarketPropertiesDTO == null || !partialMarketPropertiesDTO.Any())
            {
                return NotFound("No market properties found.");
            }

            return Ok(partialMarketPropertiesDTO);
        }

        // Allan
        [HttpGet("byMunicipality/")]
        public async Task<ActionResult<IEnumerable<MarketPropertyListingDto>>> GetMarketPropertyByMunicipality([FromQuery] string municipality)
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
        public async Task<IEnumerable<PartialMarketPropertyDTO>> GetByAgent(string agentId)
        {
            var activeListings = await _marketPropertyRepository.GetAllActiveByAgent(agentId);
            var activeListingDtos = _mapper.Map<IEnumerable<PartialMarketPropertyDTO>>(activeListings);
            return activeListingDtos;
        }

        // Adam
        [HttpDelete("{propertyId}/{agentId}")]
        [Authorize]

        public async Task<IActionResult> AgentDelete(int propertyId, string agentId)
        {
            var result = await _marketPropertyRepository.AgentDelete(propertyId, agentId);
            if (result)
            {
                return Ok("Property deleted successfully.");
            }
            else
            {
                return NotFound("Property not found or you do not have permission to delete it.");
            }
        }

        // Katarina
        [HttpGet("{MarketPropertyId}")]
        public async Task<IActionResult> GetMarketPropertyById(int MarketPropertyId)
        {
            var marketProperty = await _marketPropertyRepository.GetMarketPropertyById(MarketPropertyId);

            if (marketProperty == null)
            {
                return NotFound($"No market property found with ID {MarketPropertyId}.");
            }

            var dto = _mapper.Map<MarketPropertyDetailsDto>(marketProperty);
            return Ok(dto);
        }

        // Katarina
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMarketProperty(MarketPropertyCreateDto createDto)
        {
            var marketProperty = _mapper.Map<MarketProperty>(createDto);

            var createdProperty = await _marketPropertyRepository.CreateMarketPropertyAsync(marketProperty);

            if (createdProperty == null)
            {
                return BadRequest("Invalid MunicipalityId or RealEstateAgentId.");
            }

            var resultDto = _mapper.Map<MarketPropertyDetailsDto>(createdProperty);

            return CreatedAtAction(nameof(GetMarketPropertyById), new { MarketPropertyId = createdProperty.MarketPropertyId }, resultDto);
        }

        // Allan
        [HttpPost("filter")]
        public async Task<ActionResult<List<PartialMarketPropertyDTO>>> SearchProperties([FromBody] MarketPropertySearchDto searchDto)
        {
            var properties = await _marketPropertyRepository.SearchMarketPropertiesAsync(searchDto);

            if (!properties.Any())
                return NotFound("No properties matched the search.");

            var dtoList = _mapper.Map<List<PartialMarketPropertyDTO>>(properties);
            return Ok(dtoList);
        }

    }
}

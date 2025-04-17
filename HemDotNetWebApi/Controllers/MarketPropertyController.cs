using AutoMapper;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.DTOs;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [HttpGet]
        public async Task<IActionResult> GetAllMarketPropertiesPartial()
        {
            var partialMarketPropertiesDTO = await _marketPropertyRepository.GetAllMarketPropertiesPartial();
            if (partialMarketPropertiesDTO == null || !partialMarketPropertiesDTO.Any())
            {
                return NotFound("No market properties found.");
            }

            return Ok(partialMarketPropertiesDTO);
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

using AutoMapper;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HemDotNetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}

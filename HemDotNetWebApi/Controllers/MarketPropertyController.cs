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
        private readonly IMarketPropertyRepository _marketPropertyRepository;
        private readonly IMapper _autoMapper;

        public MarketPropertyController(IMarketPropertyRepository marketPropertyRepository, IMapper autoMapper)
        {
            _marketPropertyRepository = marketPropertyRepository;
            _autoMapper = autoMapper;
        }
        //Author: Johan Ek
        [HttpGet]
        public async Task<IActionResult> GetAllMarketProperties()
        {
            var marketProperties = await _marketPropertyRepository.GetAllMarketProperties();
            if (marketProperties == null || !marketProperties.Any())
            {
                return NotFound("No market properties found.");
            }
            // Map the MarketProperty entities to DTO, including only the desired sections.
            var partialMarketPropertiesDTO = _autoMapper.Map<IEnumerable<PartialMarketPropertyDTO>>(marketProperties);
            return Ok(partialMarketPropertiesDTO);
        }

    }
}

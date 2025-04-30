using HemDotNetWebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HemDotNetWebApi.Controllers
{
    // Author: Allan

    [ApiController]
    [Route("[controller]")]
    public class MunicipalityController : ControllerBase
    {
        // Author: Allan
        [HttpGet]
        public async Task<IActionResult> GetAllMarketMunicipalities()
        {
            /*
            var marketProperty = await _marketPropertyRepository.GetMarketPropertyById(MarketPropertyId);
            //var municipalities = away 

            if (marketProperty == null)
            {
                return NotFound($"No market property found with ID {MarketPropertyId}.");
            }

            var dto = _mapper.Map<MarketPropertyDetailsDto>(marketProperty);
            return Ok(dto);
            */
            return Ok();
        }

    }
}

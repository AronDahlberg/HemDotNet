using HemDotNetWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HemDotNetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketPropertyController : ControllerBase
    {
        private readonly IMarketPropertyRepository _marketPropertyRepository;

        public MarketPropertyController(IMarketPropertyRepository marketPropertyRepository)
        {
            _marketPropertyRepository = marketPropertyRepository;
        }
        //Author Johan Ek
        [HttpGet]
        public async Task<IActionResult> GetAllMarketProperties()
        {
            var marketProperties = await _marketPropertyRepository.GetAllMarketProperties();
            return Ok(marketProperties);
        }

    }
}

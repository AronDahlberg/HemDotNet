using AutoMapper;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<IActionResult> GetAllMarketPropertiesPartial()
        {
            //Author: Johan Ek
            var partialMarketPropertiesDTO = await _marketPropertyRepository.GetAllMarketPropertiesPartial();
            if (partialMarketPropertiesDTO == null || !partialMarketPropertiesDTO.Any())
            {
                return NotFound("No market properties found.");
            }

            return Ok(partialMarketPropertiesDTO);
        }

    }
}

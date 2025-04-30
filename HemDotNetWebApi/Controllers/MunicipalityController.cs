using AutoMapper;
using HemDotNetWebApi.Data;
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
        private readonly IMapper _mapper;
        private readonly IMunicipalityRepository _municipalityRepository;

        public MunicipalityController(IMapper mapper, IMunicipalityRepository municipalityRepository)
        {
            _mapper = mapper;
            _municipalityRepository = municipalityRepository;
        }

        // Author: Allan
        [HttpGet]
        public async Task<IActionResult> GetAllMarketMunicipalities()
        {
            var municipalities = await _municipalityRepository.GetAllMunicipalities();
            if (municipalities == null)
            {
                return NotFound($"No municipalities found.");
            }
            
            //var dto = _mapper.Map<MarketPropertyDetailsDto>(marketProperty);
            //return Ok(dto);
        }

    }
}

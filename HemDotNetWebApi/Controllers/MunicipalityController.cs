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
        public async Task<ActionResult<IEnumerable<MunicipalityDto>>> GetAllMarketMunicipalities()
        {
            var municipalities = await _municipalityRepository.GetAllMunicipalities();
            if (municipalities == null || !municipalities.Any())
            {
                return NotFound("No municipalities found.");
            }

            var dto = _mapper.Map<IEnumerable<MunicipalityDto>>(municipalities);
            return Ok(dto);
        }

        // Author: Allan
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MunicipalityDto>>> SearchMunicipalities([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var results = await _municipalityRepository.SearchMunicipalitiesAsync(searchTerm);
            var dto = _mapper.Map<IEnumerable<MunicipalityDto>>(results);
            return Ok(dto);
        }

    }
}

using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HemDotNetWebApi.Controllers
{
    // Allan
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateAgencyController : ControllerBase
    {
        private readonly IRealEstateAgencyRepository _realEstateAgencyRepository;
        private readonly IMapper _mapper;

        public RealEstateAgencyController(IRealEstateAgencyRepository realEstateAgencyRepository, IMapper mapper)
        {
            _realEstateAgencyRepository = realEstateAgencyRepository;
            _mapper = mapper;
        }

        // Allan
        [HttpGet]
        public async Task<ActionResult<List<AgencyNameDto>>> GetAgencies()
        {
            var agencies = await _realEstateAgencyRepository.GetAllAsync();

            if (agencies == null || !agencies.Any())
                return NotFound("Inga mäklarbyråer hittades");

            var agencyDtos = _mapper.Map<List<AgencyNameDto>>(agencies);

            return Ok(agencyDtos);
        }

        // Allan
        [Authorize(Roles = ApiRoles.Administrator)]
        [HttpPost]
        public async Task<ActionResult<int>> CreateAgency([FromBody] AgencyCreateDto agencyCreateDto)
        {
            try
            {
                var createdAgencyId = await _realEstateAgencyRepository.CreateAgencyAsync(agencyCreateDto);
                return Ok(createdAgencyId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ett internt fel uppstod vid skapandet av mäklarbyrån.");
            }
        }
    }
}

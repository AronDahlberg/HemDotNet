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
    [ApiController]
    [Route("[controller]")]
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
        [HttpGet("PartialAgencies")]
        public async Task<ActionResult<List<AgencyDto>>> GetPartialAgencies()
        {
            var agencies = await _realEstateAgencyRepository.GetAllAsync();

            if (agencies == null || !agencies.Any())
                return NotFound("Inga mäklarbyråer hittades");

            var agencyDtos = _mapper.Map<List<AgencyDto>>(agencies);

            return Ok(agencyDtos);
        }

        // Allan
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = ApiRoles.Administrator)]
        public async Task<IActionResult> DeleteAgency(int id)
        {
            var success = await _realEstateAgencyRepository.DeleteAgency(id);

            if (!success)
                return NotFound($"Ingen mäklarbyrå med ID {id} hittades");

            return Ok();
        }


    }
}

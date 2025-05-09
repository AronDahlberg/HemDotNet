using AutoMapper;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.DTOs;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HemDotNetWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RealEstateAgentController : ControllerBase
    {
        private readonly IRealEstateAgentRepository _realEstateAgentRepository;
        private readonly IMapper _mapper;

        public RealEstateAgentController(IRealEstateAgentRepository realEstateAgentRepository, IMapper mapper)
        {
            _realEstateAgentRepository = realEstateAgentRepository;
            _mapper = mapper;
        }


        // Chris
        // GET: RealEstateAgent/5
        [HttpGet("{agentId}")]
        public async Task<ActionResult<RealEstateAgent>> GetAgent(string agentId)
        {
            var agent = await _realEstateAgentRepository.GetAsync(agentId);

            if (agent == null)
            {
                return NotFound($"Agent with ID {agentId} not found.");
            }

            return Ok(agent);
        }

        // Allan
        [HttpGet("/GetProfile/{agentId}")]
        public async Task<ActionResult<RealEstateAgentDto>> GetAgentProfile(string agentId)
        {
            var agent = await _realEstateAgentRepository.GetAsync(agentId);

            if (agent == null)
                return NotFound($"Agent with ID {agentId} not found.");

            var dto = _mapper.Map<RealEstateAgentDto>(agent);

            return Ok(dto);
        }

        // Chris
        // PUT: RealEstateAgent/5
        [HttpPut("{agentId}")]
        public async Task<ActionResult<RealEstateAgentUpdateDTO>> Update(string agentId, RealEstateAgentUpdateDTO agentUpdateDto)
        {
            var agentToUpdate = await _realEstateAgentRepository.GetAsync(agentId);

            if (agentToUpdate == null)
            {
                return NotFound($"Agent with ID {agentId} not found.");
            }

            _mapper.Map(agentUpdateDto, agentToUpdate);

            var updatedAgent = await _realEstateAgentRepository.UpdateAsync(agentToUpdate);

            var resultDto = _mapper.Map<RealEstateAgentUpdateDTO>(updatedAgent);

            return Ok(resultDto);
        }

        //Chris
        //GET: RealEstateAgent?{searchParams}
        [HttpGet]
        public async Task<ActionResult<List<RealEstateAgentDto>>> GetAgents(
            [FromQuery] string? municipality,
            [FromQuery] string? firstName, 
            [FromQuery] string? lastName, 
            [FromQuery] string? agencyName,
            [FromQuery] string? email,
            [FromQuery] string? phonenumber
            )
        {
            var filteredAgents = await _realEstateAgentRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(municipality))
                filteredAgents = filteredAgents.Where(a => a.RealEstateAgentAgency.RealEstateAgencyName.ToLower().Contains(municipality.ToLowerInvariant()));

            if (!string.IsNullOrWhiteSpace(firstName))
                filteredAgents = filteredAgents.Where(a => a.RealEstateAgentFirstName.ToLower().Contains(firstName.ToLowerInvariant()));

            if (!string.IsNullOrWhiteSpace(lastName))
                filteredAgents = filteredAgents.Where(a => a.RealEstateAgentLastName.ToLower().Contains(lastName.ToLowerInvariant()));

            if (!string.IsNullOrWhiteSpace(agencyName))
                filteredAgents = filteredAgents.Where(a => a.RealEstateAgentAgency.RealEstateAgencyName.ToLower().Contains(agencyName.ToLowerInvariant()));

            if (!string.IsNullOrWhiteSpace(email))
                filteredAgents = filteredAgents.Where(a => a.RealEstateAgentEmail.ToLower().Contains(email.ToLowerInvariant()));

            if (!string.IsNullOrWhiteSpace(phonenumber))
                filteredAgents = filteredAgents.Where(a => a.RealEstateAgentPhoneNumber.Contains(phonenumber));


            return Ok(_mapper.Map<List<RealEstateAgentDto>>(filteredAgents.ToList()));

        }
    }
}

using AutoMapper;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTOs;
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
        public async Task<IActionResult> GetAgent(string agentId)
        {
            var agent = await _realEstateAgentRepository.GetAsync(agentId);

            if (agent == null)
            {
                return NotFound($"Agent with ID {agentId} not found.");
            }

            return Ok(agent);
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
    }
}

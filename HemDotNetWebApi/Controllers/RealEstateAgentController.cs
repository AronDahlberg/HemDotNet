using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.DTOs;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                filteredAgents = filteredAgents.Where(a => a.RealEstateAgentAgency.RealEstateAgencyMunicipality.ToLower().Contains(municipality.ToLowerInvariant()));

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

        // Allan
        [HttpPut("UpdateAgency/{agentId}/{newAgencyId}")]
        [Authorize(Roles = ApiRoles.Administrator)]
        public async Task<ActionResult<RealEstateAgentDto>> UpdateAgentAgency(string agentId, int newAgencyId)
        {
            try
            {
                var updatedAgent = await _realEstateAgentRepository.UpdateAgentAgencyAsync(agentId, newAgencyId);
                var dto = _mapper.Map<RealEstateAgentDto>(updatedAgent);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the agent's agency: {ex.Message}");
            }
        }

        // Allan
        [HttpDelete("{agentId}")]
        [Authorize(Roles = ApiRoles.Administrator)]
        public async Task<IActionResult> DeleteAgent(string agentId)
        {
            try
            {
                await _realEstateAgentRepository.DeleteAsync(agentId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ett fel inträffade: {ex.Message}");
            }
        }

        // Allan
        [HttpPost("ProfilePicture")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<ActionResult> UploadProfilePicture([FromForm] UploadAgentProfilePictureDto dto, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image file provided");
            }

            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
            string fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Invalid file type. Only jpg, jpeg and png are allowed");
            }

            if (imageFile.Length > 5 * 1024 * 1024)
            {
                return BadRequest("File size is larger than limit of 5MB");
            }

            var userId = User.FindFirstValue(CustomClaimTypes.Uid);

            if (dto.AgentId != userId)
            {
                return Forbid("You can only update your own profile picture.");
            }

            try
            {
                var imageUrl = await _realEstateAgentRepository.UploadAgentProfilePictureAsync(dto.AgentId, imageFile);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while uploading the profile picture.");
            }
        }

    }
}

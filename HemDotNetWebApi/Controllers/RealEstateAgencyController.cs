using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost("CreateAgency")]
        [Authorize(Roles = ApiRoles.Administrator)]
        public async Task<ActionResult<int>> CreateAgency([FromBody] AgencyCreateDto agencyCreateDto)
        {
            try
            {
                agencyCreateDto.RealEstateAgencyLogoUrl = "Images/DefaultAgencyImage.png";
                var createdAgencyId = await _realEstateAgencyRepository.CreateAgencyAsync(agencyCreateDto);
                return Ok(createdAgencyId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ett internt fel uppstod vid skapandet av mäklarbyrån.");
            }
        }

        // Allan
        [HttpPost("AgencyImage")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = ApiRoles.Administrator)]
        public async Task<ActionResult<AgencyImageUrlDto>> UploadAgencyImage([FromForm] int agencyId, IFormFile imageFile)
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

            try
            {
                var imageUrl = await _realEstateAgencyRepository.UploadAgencyImageAsync(agencyId, imageFile);
                var imageUrlDto = new AgencyImageUrlDto() { AgencyImageUrl = imageUrl };

                return Ok(imageUrlDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while uploading the picture.");
            }
        }
    }
}

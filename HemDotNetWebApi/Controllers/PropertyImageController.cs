using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HemDotNetWebApi.Controllers
{
    // Allan
    [ApiController]
    [Route("[controller]")]
    public class PropertyImageController : ControllerBase
    {
        private readonly IPropertyImageRepository _repository;
        private readonly IMapper _mapper;

        // Allan
        public PropertyImageController(IPropertyImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Allan
        //Commented out Authorize, since we need to GET images in the public Details page. /Johan
        //[Authorize] 
        [HttpGet("{marketPropertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetPropertyImages(int marketPropertyId)
        {
            if (!await _repository.PropertyExistsAsync(marketPropertyId))
            {
                return NotFound($"Market property with ID {marketPropertyId} was not found");
            }

            var images = await _repository.GetImagesByPropertyIdAsync(marketPropertyId);
            return Ok(_mapper.Map<IEnumerable<PropertyImageDto>>(images));
        }

        // Allan
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]

        public async Task<ActionResult<PropertyImageDto>> AddPropertyImage([FromForm] AddPropertyImageDto dto, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image file provided");
            }

            // Validate file type - which files types are allowed?
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png"};
            string fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Invalid file type. Only jpg, jpeg and png are allowed");
            }

            if (imageFile.Length > 5 * 1024 * 1024)
            {
                return BadRequest("File size is larger than limit of 5MB");
            }

            if (!await _repository.PropertyExistsAsync(dto.MarketPropertyId))
            {
                return NotFound($"Market property with ID {dto.MarketPropertyId} was not found");
            }


            var userId = User.FindFirstValue(CustomClaimTypes.Uid);

            if (!await _repository.IsPropertyOwnedByAgentAsync(dto.MarketPropertyId, userId))
            {
                return Forbid("You do not own this property.");
            }

            try
            {
                var newImage = new PropertyImage
                {
                    MarketPropertyId = dto.MarketPropertyId
                };

                var addedImage = await _repository.AddImageAsync(newImage, imageFile);
                var resultDto = _mapper.Map<PropertyImageDto>(addedImage);

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the picture");
            }
        }

        // Allan
        [HttpDelete("{imageId}")]
        [Authorize]
        public async Task<ActionResult> DeletePropertyImage(int imageId)
        {
            if (!await _repository.ImageExistsAsync(imageId))
            {
                return NotFound($"Property image with ID {imageId} not found");
            }

            try
            {
                var userId = User.FindFirstValue(CustomClaimTypes.Uid);
                var propertyId = await _repository.GetPropertyIdByImageIdAsync(imageId);

                if (propertyId == null || !await _repository.IsPropertyOwnedByAgentAsync(propertyId.Value, userId))
                {
                    return Forbid("You do not own this property or image.");
                }

                bool result = await _repository.DeleteImageAsync(imageId);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "An error occurred while deleting the image");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}

using AutoMapper;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HemDotNetWebApi.Controllers
{
    // Allan
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("getByProperty/{marketPropertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetPropertyImages(int marketPropertyId)
        {
            if (!await _repository.PropertyExistsAsync(marketPropertyId))
            {
                return NotFound($"Market property with ID {marketPropertyId} not found");
            }

            var images = await _repository.GetImagesByPropertyIdAsync(marketPropertyId);
            return Ok(_mapper.Map<IEnumerable<PropertyImageDto>>(images));
        }

        // Allan
        [HttpPost("add/")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PropertyImageDto>> AddPropertyImage([FromForm] AddPropertyImageDto dto, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image file provided");
            }

            // Validate file type
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

            try
            {
                var newImage = new PropertyImage
                {
                    MarketPropertyId = dto.MarketPropertyId
                };

                var addedImage = await _repository.AddImageAsync(newImage, imageFile);
                var resultDto = _mapper.Map<PropertyImageDto>(addedImage);

                return CreatedAtAction(nameof(GetPropertyImages), new { marketPropertyId = dto.MarketPropertyId }, resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the picture");
            }
        }

        // Allan
        [HttpDelete("delete/{imageId}")]
        public async Task<ActionResult> DeletePropertyImage(int imageId)
        {
            if (!await _repository.ImageExistsAsync(imageId))
            {
                return NotFound($"Property image with ID {imageId} not found");
            }

            try
            {
                bool result = await _repository.DeleteImageAsync(imageId);
                if (result)
                {
                    return NoContent();
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

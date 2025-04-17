using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _imageDirectory;

        public PropertyImageRepository(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            //_imageDirectory = Path.Combine(_environment.WebRootPath, "images", "properties");
            _imageDirectory = Path.Combine(_environment.WebRootPath, "Images");

            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }
        }

        public async Task<List<PropertyImage>> GetImagesByPropertyIdAsync(int marketPropertyId)
        {
            return await _context.PropertyImages
                .Where(i => i.MarketProperty.MarketPropertyId == marketPropertyId)
                .ToListAsync();
        }

        public async Task<PropertyImage> AddImageAsync(PropertyImage image, IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string filePath = Path.Combine(_imageDirectory, fileName);
            string relativePath = $"/images/properties/{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            image.PropertyImageUrl = relativePath;

            _context.PropertyImages.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _context.PropertyImages.FindAsync(imageId);
            if (image == null)
            {
                return false;
            }

            string filePath = Path.Combine(_environment.WebRootPath, image.PropertyImageUrl.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.PropertyImages.Remove(image);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ImageExistsAsync(int imageId)
        {
            return await _context.PropertyImages.AnyAsync(pi => pi.PropertyImageId == imageId);
        }

        public async Task<bool> PropertyExistsAsync(int marketPropertyId)
        {
            return await _context.MarketProperties.AnyAsync(mp => mp.MarketPropertyId == marketPropertyId);
        }
    }
}

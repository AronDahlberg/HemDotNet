using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _imageDirectory;

        // Allan
        public PropertyImageRepository(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _imageDirectory = Path.Combine(_environment.ContentRootPath, "Images", "PropertyImages");
            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }
        }

        // Allan
        public async Task<List<PropertyImage>> GetImagesByPropertyIdAsync(int marketPropertyId)
        {
            return await _context.PropertyImages
                .Where(i => i.MarketProperty.MarketPropertyId == marketPropertyId)
                .ToListAsync();
        }

        // Allan
        public async Task<PropertyImage> AddImageAsync(PropertyImage image, IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string filePath = Path.Combine(_imageDirectory, fileName);
            string relativePath = $"/Images/PropertyImages/{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            image.PropertyImageUrl = relativePath;

            _context.PropertyImages.Add(image);
            await _context.SaveChangesAsync();

            // reload complete entity with relationships, without this, marketpropertyid
            // doesnt follow
            var savedImage = await _context.PropertyImages
                .Include(pi => pi.MarketProperty)
                .FirstOrDefaultAsync(pi => pi.PropertyImageId == image.PropertyImageId);

            return savedImage;
        }

        // Allan
        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _context.PropertyImages.FindAsync(imageId);
            if (image == null)
            {
                return false;
            }

            string filePath = Path.Combine(_environment.ContentRootPath, image.PropertyImageUrl.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.PropertyImages.Remove(image);
            await _context.SaveChangesAsync();

            return true;
        }

        // Allan
        public async Task<bool> ImageExistsAsync(int imageId)
        {
            return await _context.PropertyImages.AnyAsync(i => i.PropertyImageId == imageId);
        }

        // Allan
        public async Task<bool> PropertyExistsAsync(int marketPropertyId)
        {
            return await _context.MarketProperties.AnyAsync(m => m.MarketPropertyId == marketPropertyId);
        }
    }
}

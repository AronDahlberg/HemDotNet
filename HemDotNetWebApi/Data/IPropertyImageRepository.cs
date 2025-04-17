using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IPropertyImageRepository
    {
        Task<List<PropertyImage>> GetImagesByPropertyIdAsync(int marketPropertyId);
        Task<PropertyImage> AddImageAsync(PropertyImage image, IFormFile file);
        Task<bool> DeleteImageAsync(int imageId);
        Task<bool> ImageExistsAsync(int imageId);
        Task<bool> PropertyExistsAsync(int marketPropertyId);
    }
}

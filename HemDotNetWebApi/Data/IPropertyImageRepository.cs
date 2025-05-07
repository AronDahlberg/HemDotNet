using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    // Allan
    public interface IPropertyImageRepository
    {
        // Allan
        Task<List<PropertyImage>> GetImagesByPropertyIdAsync(int marketPropertyId);
        Task<PropertyImage> AddImageAsync(PropertyImage image, IFormFile file);
        Task<bool> DeleteImageAsync(int imageId);
        Task<bool> ImageExistsAsync(int imageId);
        Task<bool> PropertyExistsAsync(int marketPropertyId);
        Task<bool> IsPropertyOwnedByAgentAsync(int marketPropertyId, string agentUserId);
        Task<int?> GetPropertyIdByImageIdAsync(int imageId);
    }
}

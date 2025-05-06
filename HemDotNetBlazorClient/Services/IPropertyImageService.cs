using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    public interface IPropertyImageService
    {
        Task<Response<List<PropertyImageDto>>> GetPropertyImages(int marketPropertyId);
        Task<Response<PropertyImageDto>> AddPropertyImage(int marketPropertyId, StreamContent fileContent);
        Task<Response<bool>> DeletePropertyImage(int imageId);
    }
}

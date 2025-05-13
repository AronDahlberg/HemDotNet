using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IRealEstateAgencyRepository
    {
        // Allan
        Task<IEnumerable<RealEstateAgency>> GetAllAsync();
        Task<int> CreateAgencyAsync(AgencyCreateDto dto);
        Task<string> UploadAgencyImageAsync(int agencyId, IFormFile file);
    }
}

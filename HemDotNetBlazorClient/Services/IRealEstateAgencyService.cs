using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IRealEstateAgencyService
    {
        // Allan
        Task<Response<List<AgencyNameDto>>> GetAllAgencies();
        Task<Response<bool>> CreateAgency(AgencyCreateDto dto);
        Task<Response<AgencyImageUrlDto>> UploadAgencyImage(int agencyId, StreamContent fileContent);
    }
}

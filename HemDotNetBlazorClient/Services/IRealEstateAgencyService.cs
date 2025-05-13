using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IRealEstateAgencyService
    {
        // Allan
        Task<Response<List<AgencyNameDto>>> GetAllAgencies();
        Task<Response<List<AgencyDto>>> GetAllAgenciesPartial();
        Task<Response<bool>> DeleteAgency(int id)
    }
}

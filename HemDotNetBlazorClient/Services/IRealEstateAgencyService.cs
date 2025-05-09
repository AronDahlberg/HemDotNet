using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IRealEstateAgencyService
    {
        // Allan
        Task<Response<List<AgencyNameDto>>> GetAllAgencies();
    }
}

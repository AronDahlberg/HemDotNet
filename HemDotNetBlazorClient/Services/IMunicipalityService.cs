using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IMunicipalityService
    {
        Task<Response<List<MunicipalityNameDto>>> MunicipalitiesSearch(string searchTerm);
    }
}

using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IMunicipalityService
    {
        Task<Response<List<MunicipalityDto>>> MunicipalitiesSearch(string searchTerm);

        //Co-Author: Johan
        Task<Response<List<MunicipalityDto>>> GetAllMunicipalities();
    }
}

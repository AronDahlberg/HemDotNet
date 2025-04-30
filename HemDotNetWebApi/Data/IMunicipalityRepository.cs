using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IMunicipalityRepository
    {
        // Allan
        public Task<IEnumerable<Municipality>> GetAllMunicipalities();

        // Allan
        public Task<IEnumerable<Municipality>> SearchMunicipalitiesAsync(string searchTerm);
    }
}

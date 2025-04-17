using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IMarketPropertyRepository
    {

        // Allan
        Task<IEnumerable<MarketProperty>> GetAllByMunicipality(string municipality);

        // CHRIS
        Task<IEnumerable<MarketProperty>> GetAllActiveByAgent(int agentId);

    }
}

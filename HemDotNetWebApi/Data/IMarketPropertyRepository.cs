using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IMarketPropertyRepository
    {
        //Author: Johan Ek
        Task<IEnumerable<PartialMarketPropertyDTO>> GetAllMarketPropertiesPartial();

        // Allan
        Task<IEnumerable<MarketProperty>> GetAllByMunicipality(string municipality);

        // CHRIS
        Task<IEnumerable<MarketProperty>> GetAllActiveByAgent(int agentId);

        // Adam
        Task<bool> AgentDelete(int propertyId, int agentId);
    }
}

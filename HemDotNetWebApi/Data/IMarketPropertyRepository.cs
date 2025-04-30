using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IMarketPropertyRepository
    {
        // Allan
        Task<MarketProperty> UpdateMarketProperty(MarketProperty marketProperty);

        //Author: Johan Ek
        Task<IEnumerable<PartialMarketPropertyDTO>> GetAllMarketPropertiesPartial();

        // Allan
        Task<IEnumerable<MarketProperty>> GetAllByMunicipality(string municipality);

        // CHRIS
        Task<IEnumerable<MarketProperty>> GetAllActiveByAgent(string agentId);

        // Adam
        Task<bool> AgentDelete(int propertyId, string agentId);

        // Katarina
        Task<MarketProperty?> GetMarketPropertyById(int id);

        // Katarina
        Task<MarketProperty> CreateMarketPropertyAsync(MarketProperty marketProperty);

        Task<List<MarketProperty>> SearchMarketPropertiesAsync(MarketPropertySearchDto searchDto);

    }
}

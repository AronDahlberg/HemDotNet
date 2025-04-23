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
        Task<IEnumerable<MarketProperty>> GetAllActiveByAgent(int agentId);


        // Katarina
        Task<MarketProperty?> GetMarketPropertyById(int id);

        // Katarina
        Task<MarketProperty> CreateMarketPropertyAsync(MarketProperty marketProperty);

    }
}

using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IMarketPropertyRepository
    {
        // CHRIS
        Task<IEnumerable<MarketProperty>> GetAllActiveByAgent(int agentId);
    }
}

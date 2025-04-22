using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IRealEstateAgentRepository
    {
        // CHRIS
        Task<IEnumerable<RealEstateAgent>> Get(int agentId);
    }
}

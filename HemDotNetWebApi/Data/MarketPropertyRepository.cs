using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class MarketPropertyRepository: IMarketPropertyRepository
    {

        private readonly ApplicationDbContext _context;

        // Constructor with DI to inject ApplicationDbContext
        public MarketPropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // CHRIS (TODO: only get active ones)
        public async Task<IEnumerable<MarketProperty>> GetAllActiveByAgent(int agentId)
        {
            return await _context.MarketProperties.Where(p => p.RealEstateAgent.RealEstateAgentId == agentId).ToListAsync();
        }
    }
}

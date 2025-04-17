using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class MarketPropertyRepository : IMarketPropertyRepository
    {

        private readonly ApplicationDbContext _context;

        // Constructor with DI to inject ApplicationDbContext
        public MarketPropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        // Allan
        public async Task<IEnumerable<MarketProperty>> GetAllByMunicipality(string municipality)
        {
            return await _context.MarketProperties
                .Include(p => p.Municipality)
                .Include(p => p.Images)
                .Where(p => p.Municipality.MunicipalityName == municipality).ToListAsync();
        }
        
        // CHRIS (TODO: only get active ones)
        public async Task<IEnumerable<MarketProperty>> GetAllActiveByAgent(int agentId)
        {
            return await _context.MarketProperties
                .Where(p => p.RealEstateAgent.RealEstateAgentId == agentId)
                .Include(p => p.Municipality)
                .ToListAsync();

        }
    }
}

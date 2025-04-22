using AutoMapper;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class RealEstateAgentRepository : IRealEstateAgentRepository
    {
        private readonly ApplicationDbContext _context;

        public RealEstateAgentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RealEstateAgent>> Get(int agentId)
        {
            return await _context.RealEstateAgents
                .Where(p => p.RealEstateAgentId == agentId)
                .ToListAsync();
        }
    }
}

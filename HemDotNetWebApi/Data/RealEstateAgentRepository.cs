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

        // CHRIS
        public async Task<RealEstateAgent> GetAsync(int agentId)
        {
            return await _context.RealEstateAgents
                .Include(a => a.RealEstateAgentAgency)
                .Include(a => a.RealEstateAgentProperties)
                .FirstOrDefaultAsync(a => a.RealEstateAgentId == agentId);
        }

        // CHRIS
        public async Task<RealEstateAgent> UpdateAsync(RealEstateAgent agent)
        {
            var existingAgent = await _context.RealEstateAgents
                .FirstOrDefaultAsync(a => a.RealEstateAgentId == agent.RealEstateAgentId);

            if (existingAgent == null)
            {
                throw new KeyNotFoundException($"Agent with ID {agent.RealEstateAgentId} not found.");
            }

            _context.Entry(existingAgent).CurrentValues.SetValues(agent);

            await _context.SaveChangesAsync();

            return existingAgent;
        }

    }
}

using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class RealEstateAgentRepository : IRealEstateAgentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RealEstateAgentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // CHRIS
        // Co-Author: Allan - take away accounts with role admin from what we return
        public async Task<IEnumerable<RealEstateAgent>> GetAllAsync()
        {

            var agents = await _context.RealEstateAgents
                .Include(a => a.RealEstateAgentAgency)
                .Include(a => a.RealEstateAgentProperties)
                .Where(agent =>
                    !_context.UserRoles
                        .Where(ur => ur.UserId == agent.Id)
                        .Join(_context.Roles,
                              ur => ur.RoleId,
                              r => r.Id,
                              (ur, r) => r.Name)
                        .Contains(ApiRoles.Administrator))
                .ToListAsync();


            return agents;
        }

        // CHRIS
        public async Task<RealEstateAgent> GetAsync(string agentId)
        {
            return await _context.RealEstateAgents
                .Include(a => a.RealEstateAgentAgency)
                .Include(a => a.RealEstateAgentProperties)
                .FirstOrDefaultAsync(a => a.Id == agentId);
        }

        // CHRIS
        public async Task<RealEstateAgent> UpdateAsync(RealEstateAgent agent)
        {
            var existingAgent = await _context.RealEstateAgents
                .FirstOrDefaultAsync(a => a.Id == agent.Id);

            if (existingAgent == null)
            {
                throw new KeyNotFoundException($"Agent with ID {agent.Id} not found.");
            }

            _context.Entry(existingAgent).CurrentValues.SetValues(agent);

            await _context.SaveChangesAsync();

            return existingAgent;
        }

    }
}

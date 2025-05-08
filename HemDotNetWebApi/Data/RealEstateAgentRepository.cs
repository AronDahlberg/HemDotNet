using AutoMapper;
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
        public async Task<IEnumerable<RealEstateAgentDto>> GetAllAsync()
        {
            var agents = await _context.RealEstateAgents
                .Include(a => a.RealEstateAgentAgency)
                .Include(a => a.RealEstateAgentProperties)
                .ToListAsync();

            var agentDtos = _mapper.Map<IEnumerable<RealEstateAgentDto>>(agents);

            return agentDtos;
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

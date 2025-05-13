using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    // Allan
    public class RealEstateAgencyRepository : IRealEstateAgencyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RealEstateAgencyRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Allan
        public async Task<IEnumerable<RealEstateAgency>> GetAllAsync()
        {
            return await _context.RealEstateAgencies.Include(a => a.RealEstateAgencyAgents).ToListAsync();
        }

        // Allan
        public async Task<bool> DeleteAgency(int id)
        {
            // We want to delete an agency, but the agency has agents; what to do?
            // here, we decide to assign those agents to the wait list.
            var agency = await _context.RealEstateAgencies
                .Include(a => a.RealEstateAgencyAgents)
                .FirstOrDefaultAsync(a => a.RealEstateAgencyId == id);

            if (agency == null)
                return false;

            var waitListAgency = await _context.RealEstateAgencies
                .FirstOrDefaultAsync(a => a.RealEstateAgencyName == "Wait List");

            if (waitListAgency == null)
                throw new InvalidOperationException("Wait List agency not found. Please create one before deleting agencies.");

            
            foreach (var agent in agency.RealEstateAgencyAgents)
            {
                agent.RealEstateAgentAgencyId = waitListAgency.RealEstateAgencyId;
            }

            _context.RealEstateAgencies.Remove(agency);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

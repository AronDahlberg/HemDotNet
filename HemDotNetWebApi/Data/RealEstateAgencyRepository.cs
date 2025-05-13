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
            return await _context.RealEstateAgencies.ToListAsync();
        }

        public async Task<bool> DeleteAgency(int id)
        {
            var agency = await _context.RealEstateAgencies.FindAsync(id);
            if (agency == null)
            {
                return false;
            }

            _context.RealEstateAgencies.Remove(agency);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

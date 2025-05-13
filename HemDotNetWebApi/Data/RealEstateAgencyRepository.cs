using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.DTO;
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

        // Allan
        public async Task<int> CreateAgencyAsync(AgencyCreateDto dto)
        {
            var agencyToCreate = _mapper.Map<RealEstateAgency>(dto);
            _context.RealEstateAgencies.Add(agencyToCreate);
            await _context.SaveChangesAsync();
            return agencyToCreate.RealEstateAgencyId;
        }
    }
}

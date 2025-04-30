
using AutoMapper;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    // Allan
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MunicipalityRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Allan
        public async Task<IEnumerable<Municipality>> GetAllMunicipalities()
        {
            return await _context.Municipalities.ToListAsync();
        }

        public async Task<IEnumerable<Municipality>> SearchMunicipalitiesAsync(string searchTerm)
        {
            return await _context.Municipalities
                .Where(m => m.MunicipalityName.StartsWith(searchTerm))
                .OrderBy(m => m.MunicipalityName)
                .Take(10)
                .ToListAsync();
        }
    }
}

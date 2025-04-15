using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class MarketPropertyRepository : IMarketPropertyRepository
    {

        private readonly ApplicationDbContext _context;

        public MarketPropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Allan
        public async Task<IEnumerable<MarketProperty>> GetAllByMunicipality(string municipality)
        {
            return await _context.MarketProperties
                .Include(x => x.Municipality)
                .Include(x => x.Images)
                .Where(p => p.Municipality.MunicipalityName == municipality).ToListAsync();
        }
    }
}

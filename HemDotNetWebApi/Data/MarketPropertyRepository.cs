using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class MarketPropertyRepository : IMarketPropertyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MarketPropertyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        //Author: Johan Ek
        public async Task<IEnumerable<MarketProperty>> GetAllMarketProperties()
        {
            return await _applicationDbContext.MarketProperties.ToListAsync();
        }
    }
}

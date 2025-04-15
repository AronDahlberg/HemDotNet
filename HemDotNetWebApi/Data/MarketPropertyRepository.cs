using AutoMapper;
using AutoMapper.QueryableExtensions;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class MarketPropertyRepository : IMarketPropertyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _autoMapper;

        public MarketPropertyRepository(ApplicationDbContext applicationDbContext, IMapper autoMapper)
        {
            _applicationDbContext = applicationDbContext;
            _autoMapper = autoMapper;
        }
        public async Task<IEnumerable<PartialMarketPropertyDTO>> GetAllMarketPropertiesPartial()
        {
            //Author: Johan Ek
            //Gets all MarketProperties, eagerly loads Municipality and Images, then projects to a DTO.
            return await _applicationDbContext.MarketProperties
                .Include(mp => mp.Municipality)
                .Include(mp => mp.Images)
                .ProjectTo<PartialMarketPropertyDTO>(_autoMapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}

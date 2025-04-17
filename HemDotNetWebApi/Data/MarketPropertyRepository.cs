using AutoMapper;
using AutoMapper.QueryableExtensions;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class MarketPropertyRepository : IMarketPropertyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MarketPropertyRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PartialMarketPropertyDTO>> GetAllMarketPropertiesPartial()
        {
            //Author: Johan Ek
            //Gets all MarketProperties, eagerly loads Municipality and Images, then projects to a DTO.
            return await _context.MarketProperties
                .Include(mp => mp.Municipality)
                .Include(mp => mp.Images)
                .ProjectTo<PartialMarketPropertyDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}

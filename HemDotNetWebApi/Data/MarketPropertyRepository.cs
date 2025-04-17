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
        
        //Author: Johan Ek
        public async Task<IEnumerable<PartialMarketPropertyDTO>> GetAllMarketPropertiesPartial()
        {
            //Gets all MarketProperties, eagerly loads Municipality and Images, then projects to a DTO.
            return await _context.MarketProperties
                .Include(mp => mp.Municipality)
                .Include(mp => mp.Images)
                .ProjectTo<PartialMarketPropertyDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // Allan
        public async Task<IEnumerable<MarketProperty>> GetAllByMunicipality(string municipality)
        {
            return await _context.MarketProperties
                .Include(p => p.Municipality)
                .Include(p => p.Images)
                .Where(p => p.Municipality.MunicipalityName == municipality).ToListAsync();
        }
        
        // CHRIS (TODO: only get active ones)
        public async Task<IEnumerable<MarketProperty>> GetAllActiveByAgent(int agentId)
        {
            return await _context.MarketProperties
                .Where(p => p.RealEstateAgent.RealEstateAgentId == agentId)
                .Include(p => p.Municipality)
                .ToListAsync();
        }
    }
}

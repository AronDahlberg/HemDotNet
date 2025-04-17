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

        // Allan
        public async Task<MarketProperty> UpdateMarketProperty(MarketProperty marketProperty)
        {
            var existingProperty = await _context.MarketProperties
                .Include(mp => mp.Municipality)
                .Include(mp => mp.RealEstateAgent)
                .FirstOrDefaultAsync(mp => mp.MarketPropertyId == marketProperty.MarketPropertyId);

            if (existingProperty == null)
            {
                return null;
            }

            // We don't allow changing id
            marketProperty.MarketPropertyId = existingProperty.MarketPropertyId;

            // we say to EF: "forget about this object"
            _context.Entry(existingProperty).State = EntityState.Detached;

            // this tells EF: here's a new object representing a row in the database. Treat all its properties as changed,
            // and update them in the database. Generated SQL Update query for this row
            _context.Entry(marketProperty).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return marketProperty;
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

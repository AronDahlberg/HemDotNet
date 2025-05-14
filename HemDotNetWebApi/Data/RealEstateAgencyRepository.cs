using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HemDotNetWebApi.Data
{
    // Allan
    public class RealEstateAgencyRepository : IRealEstateAgencyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly string _agencyImageDirectory;

        public RealEstateAgencyRepository(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;

            _agencyImageDirectory = Path.Combine(_environment.ContentRootPath, "Images", "AgencyImages");
            if (!Directory.Exists(_agencyImageDirectory))
            {
                Directory.CreateDirectory(_agencyImageDirectory);
            }
        }

        // Allan
        public async Task<IEnumerable<RealEstateAgency>> GetAllAsync()
        {
            return await _context.RealEstateAgencies.Include(a => a.RealEstateAgencyAgents).ToListAsync();
        }

        // Allan
        public async Task<bool> DeleteAgency(int id)
        {
            // Options 1: we dont delete the agents linked to an agency, we move them to the wait list

            //var agency = await _context.RealEstateAgencies
            //    .Include(a => a.RealEstateAgencyAgents)
            //    .FirstOrDefaultAsync(a => a.RealEstateAgencyId == id);

            //if (agency == null)
            //    return false;

            //var waitListAgency = await _context.RealEstateAgencies
            //    .FirstOrDefaultAsync(a => a.RealEstateAgencyName == "Wait List");

            //if (waitListAgency == null)
            //    throw new InvalidOperationException("Wait List agency not found. Please create one before deleting agencies.");


            //foreach (var agent in agency.RealEstateAgencyAgents)
            //{
            //    agent.RealEstateAgentAgencyId = waitListAgency.RealEstateAgencyId;
            //}

            //_context.RealEstateAgencies.Remove(agency);

            //await _context.SaveChangesAsync();
            //return true;

            // Option 2, we delete the agents and their properties

            var agency = await _context.RealEstateAgencies
            .Include(a => a.RealEstateAgencyAgents)
            .FirstOrDefaultAsync(a => a.RealEstateAgencyId == id);

                    if (agency == null)
                        return false;

                    _context.RealEstateAgents.RemoveRange(agency.RealEstateAgencyAgents);
                    _context.RealEstateAgencies.Remove(agency);
                    await _context.SaveChangesAsync();
            return true;
        }

        // Allan
        public async Task<int> CreateAgencyAsync(AgencyCreateDto dto)
        {
            var swedishCulture = new CultureInfo("sv-SE");

            var candidateMunicipalities = await _context.Municipalities
                .Where(m => m.MunicipalityName.ToLower() == dto.RealEstateAgencyMunicipality.ToLower())
                .ToListAsync();

            var municipality = candidateMunicipalities
                .FirstOrDefault(m => string.Compare(m.MunicipalityName, dto.RealEstateAgencyMunicipality,
                                                    ignoreCase: true, culture: swedishCulture) == 0);

            if (municipality == null)
                throw new Exception("Kommunen finns inte i databasen");

            dto.RealEstateAgencyMunicipality = municipality.MunicipalityName;

            var agencyToCreate = _mapper.Map<RealEstateAgency>(dto);
            _context.RealEstateAgencies.Add(agencyToCreate);
            await _context.SaveChangesAsync();
            return agencyToCreate.RealEstateAgencyId;
        }

        // Allan
        public async Task<string> UploadAgencyImageAsync(int agencyId, IFormFile file)
        {
            var agency = await _context.RealEstateAgencies.FindAsync(agencyId);

            if (agency == null)
                throw new Exception("Agent not found");

            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string filePath = Path.Combine(_agencyImageDirectory, fileName);
            string relativePath = $"Images/AgencyImages/{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            agency.RealEstateAgencyLogoUrl = relativePath;

            _context.RealEstateAgencies.Update(agency);
            await _context.SaveChangesAsync();

            return relativePath;
        }
    }
}

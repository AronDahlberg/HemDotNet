using AutoMapper;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class RealEstateAgentRepository : IRealEstateAgentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly string _profileImageDirectory;

        public RealEstateAgentRepository(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
            _profileImageDirectory = Path.Combine(_environment.ContentRootPath, "Images", "ProfileImages");
            if (!Directory.Exists(_profileImageDirectory))
            {
                Directory.CreateDirectory(_profileImageDirectory);
            }
        }

        // CHRIS
        // Co-Author: Allan - take away accounts with role admin from what we return
        public async Task<IEnumerable<RealEstateAgent>> GetAllAsync()
        {

            var agents = await _context.RealEstateAgents
                .Include(a => a.RealEstateAgentAgency)
                .Include(a => a.RealEstateAgentProperties)
                .Where(agent =>
                    !_context.UserRoles
                        .Where(ur => ur.UserId == agent.Id)
                        .Join(_context.Roles,
                              ur => ur.RoleId,
                              r => r.Id,
                              (ur, r) => r.Name)
                        .Contains(ApiRoles.Administrator))
                .ToListAsync();


            return agents;
        }

        // CHRIS
        public async Task<RealEstateAgent> GetAsync(string agentId)
        {
            return await _context.RealEstateAgents
                .Include(a => a.RealEstateAgentAgency)
                .Include(a => a.RealEstateAgentProperties)
                .FirstOrDefaultAsync(a => a.Id == agentId);
        }

        // CHRIS
        public async Task<RealEstateAgent> UpdateAsync(RealEstateAgent agent)
        {
            var existingAgent = await _context.RealEstateAgents
                .FirstOrDefaultAsync(a => a.Id == agent.Id);

            if (existingAgent == null)
            {
                throw new KeyNotFoundException($"Agent with ID {agent.Id} not found.");
            }

            agent.Email = existingAgent.RealEstateAgentEmail;
            agent.UserName = agent.RealEstateAgentEmail;
            agent.NormalizedUserName = agent.RealEstateAgentEmail.ToUpperInvariant();
            agent.NormalizedEmail = agent.NormalizedUserName;

            _context.Entry(existingAgent).CurrentValues.SetValues(agent);

            await _context.SaveChangesAsync();

            return existingAgent;
        }

        // Allan
        public async Task<RealEstateAgent> UpdateAgentAgencyAsync(string agentId, int newAgencyId)
        {
            var agent = await _context.RealEstateAgents
                .Include(a => a.RealEstateAgentAgency)
                .FirstOrDefaultAsync(a => a.Id == agentId);

            if (agent == null)
            {
                throw new KeyNotFoundException($"Agent with ID {agentId} not found.");
            }

            var newAgency = await _context.RealEstateAgencies
                .FirstOrDefaultAsync(a => a.RealEstateAgencyId == newAgencyId);

            if (newAgency == null)
            {
                throw new KeyNotFoundException($"Agency with ID {newAgencyId} not found.");
            }

            agent.RealEstateAgentAgencyId = newAgencyId;
            agent.RealEstateAgentAgency = newAgency;

            await _context.SaveChangesAsync();

            return agent;
        }

        // Allan
        public async Task DeleteAsync(string agentId)
        {
            var agent = await _context.RealEstateAgents
                .Include(a => a.RealEstateAgentProperties)
                .FirstOrDefaultAsync(a => a.Id == agentId);

            if (agent == null)
            {
                throw new KeyNotFoundException($"Agent with ID {agentId} not found.");
            }

            _context.MarketProperties.RemoveRange(agent.RealEstateAgentProperties);

            _context.RealEstateAgents.Remove(agent);
            await _context.SaveChangesAsync();
        }

        // Allan
        public async Task<string> UploadAgentProfilePictureAsync(string agentId, IFormFile file)
        {
            var agent = await _context.RealEstateAgents.FindAsync(agentId);
            if (agent == null)
                throw new Exception("Agent not found");

            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string filePath = Path.Combine(_profileImageDirectory, fileName);
            string relativePath = $"Images/ProfileImages/{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            agent.RealEstateAgentImageUrl = relativePath;

            _context.RealEstateAgents.Update(agent);
            await _context.SaveChangesAsync();

            return relativePath;
        }

    }
}

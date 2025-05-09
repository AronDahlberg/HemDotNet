﻿using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IRealEstateAgentRepository
    {
        // CHRIS
        Task<RealEstateAgent> UpdateAsync(RealEstateAgent agent);
        
        // CHRIS
        Task<RealEstateAgent> GetAsync(string agentId);

        // CHRIS
        Task<IEnumerable<RealEstateAgentDto>> GetAllAsync();

    }
}

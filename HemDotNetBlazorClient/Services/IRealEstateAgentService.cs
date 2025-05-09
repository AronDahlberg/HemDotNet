using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IRealEstateAgentService
    {
        // Allan
        Task<Response<RealEstateAgentDto>> GetAgentByIdAsync(string agentId);

        // Chris
        Task<Response<IEnumerable<RealEstateAgentDto>>> GetAgentsAsync(string? municipality, string? firstName, string? lastName, string? agencyName, string? email, string? phoneNumber);

        Task<Response<string>> GetProfileImageUrl(string agentId);

        // Allan
        Task<Response<RealEstateAgentDto>> UpdateAgentAgencyAsync(string agentId, int newAgencyId);

    }
}

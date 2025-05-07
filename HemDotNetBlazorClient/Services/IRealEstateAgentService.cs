using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IRealEstateAgentService
    {
        Task<Response<RealEstateAgentDto>> GetAgentByIdAsync(string agentId);

        Task<Response<IEnumerable<RealEstateAgentDto>>> GetAgentsAsync(string? firstName, string? lastName, string? agencyName, string? email, string? phoneNumber);
    }
}

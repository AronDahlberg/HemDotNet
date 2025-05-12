using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IRealEstateAgentService
    {
        // Chris
        Task<Response<IEnumerable<RealEstateAgentDto>>> GetAgentsAsync(string? municipality, string? firstName, string? lastName, string? agencyName, string? email, string? phoneNumber);

        Task<Response<string>> GetProfileImageUrl(string agentId);

        // Allan
        Task<Response<RealEstateAgentDto>> UpdateAgentAgencyAsync(string agentId, int newAgencyId);
        Task<Response<bool>> DeleteAgentAsync(string agentId);
        Task<Response<bool>> EditAgentProfile(string agentid, RealEstateAgentUpdateDTO dto);
        Task<Response<RealEstateAgentDto>> GetAgentByIdAsync(string agentId);
    }
}

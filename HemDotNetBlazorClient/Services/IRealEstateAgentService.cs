using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IRealEstateAgentService
    {
        // Allan
        Task<Response<RealEstateAgentDto>> GetAgentByIdAsync(string agentId);
        Task<Response<string>> GetProfileImageUrl(string agentId);
    }
}

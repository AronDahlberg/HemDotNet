using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IRealEstateAgentService
    {
        Task<Response<RealEstateAgentDto>> GetAgentByIdAsync(string agentId);
    }
}

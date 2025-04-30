using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IMarketPropertyService
    {
        Task<Response<List<PartialMarketPropertyDTO>>> GetMarketProperties();
    }
}

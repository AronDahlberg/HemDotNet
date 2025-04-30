using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IMarketPropertyService
    {
        // Allan
        Task<Response<List<PartialMarketPropertyDTO>>> GetMarketProperties();

        // Allan
        Task<Response<List<PartialMarketPropertyDTO>>> SearchMarketProperties(MarketPropertySearchDto searchDto);
    }
}

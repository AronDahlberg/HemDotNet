using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public interface IMarketPropertyService
    {
        // Allan
        Task<Response<List<PartialMarketPropertyDTO>>> GetMarketProperties();
        Task<Response<List<PartialMarketPropertyDTO>>> SearchMarketProperties(MarketPropertySearchDto searchDto);
        Task<Response<List<PartialMarketPropertyDTO>>> GetMarketPropertiesByAgent(string agentId);
        //Author: Johan
        Task<int> CreateMarketProperty(MarketPropertyCreateDto newMarketProperty);
        //Author: Johan
        Task<MarketPropertyDetailsDto> GetMarketPropertyById(int marketPropertyId);
    }
}

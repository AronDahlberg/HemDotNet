using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public class MarketPropertyService : BaseHttpService, IMarketPropertyService
    {
        private readonly IClient _client;

        public MarketPropertyService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
            _client = client;
        }

        // Author: Allan
        public async Task<Response<List<PartialMarketPropertyDTO>>> GetMarketProperties()
        {
            Response<List<PartialMarketPropertyDTO>> response;

            try
            {
                await GetBearerToken();

                var data = await _client.MarketPropertiesAsync();
                response = new Response<List<PartialMarketPropertyDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<PartialMarketPropertyDTO>>(ex);
            }

            return response;
        }

        // Author: Allan
        public async Task<Response<List<PartialMarketPropertyDTO>>> SearchMarketProperties(MarketPropertySearchDto searchDto)
        {
            Response<List<PartialMarketPropertyDTO>> response;

            try
            {
                //await GetBearerToken();

                var data = await _client.FilterAsync(searchDto);
                response = new Response<List<PartialMarketPropertyDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<PartialMarketPropertyDTO>>(ex);
            }

            return response;
        }


        // CHRIS
        public async Task<Response<List<PartialMarketPropertyDTO>>> GetMarketPropertiesByAgent(string agentId)
        {
            Response<List<PartialMarketPropertyDTO>> response;

            try
            {
                //await GetBearerToken();

                var data = await _client.ByAgentAsync(agentId);
                
                response = new Response<List<PartialMarketPropertyDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<PartialMarketPropertyDTO>>(ex);
            }

            return response;
        }
    }
}

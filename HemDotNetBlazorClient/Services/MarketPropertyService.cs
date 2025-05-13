using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //Author: Johan
        public async Task<Response<int>> CreateMarketProperty(MarketPropertyCreateDto newMarketProperty)
        {
            Response<int> response;
            try
            {
                //await GetBearerToken();

                var marketPropertyId = await _client.MarketPropertyPOSTAsync(newMarketProperty);
                response = new Response<int>
                {
                    Data = marketPropertyId,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return response = ConvertApiExceptions<int>(ex);
            }

            return response;
        }

        //Author: Johan
        public async Task<Response<MarketPropertyDetailsDto>> GetMarketPropertyById(int marketPropertyId)
        {
            Response<MarketPropertyDetailsDto> response;

            try
            {
                //await GetBearerToken();

                var marketProperty = await _client.MarketPropertyGETAsync(marketPropertyId);
                response = new Response<MarketPropertyDetailsDto>
                {
                    Data = marketProperty,
                    Success = true
                };

            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<MarketPropertyDetailsDto>(ex);
            }

            return response;
        }

        //Author: Johan
        public async Task<Response<MarketPropertyDetailsDto>> UpdateMarketProperty(MarketPropertyUpdateDto updateDto)
        {
            Response<MarketPropertyDetailsDto> response;

            try
            {
                //await GetBearerToken();

                var marketProperty = await _client.MarketPropertyPUTAsync(updateDto);
                response = new Response<MarketPropertyDetailsDto>
                {
                    Data = marketProperty,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<MarketPropertyDetailsDto>(ex);
            }

            return response;
        }

        //Author: Allan
        public async Task<Response<bool>> DeleteMarketPropertyById(int marketPropertyId, string agentId)
        {
            Response<bool> response;

            try
            {
                await GetBearerToken();

                await _client.MarketPropertyDELETEAsync(marketPropertyId, agentId);
                response = new Response<bool>
                {
                    Data = true,
                    Success = true
                };

            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<bool>(ex);
            }

            return response;
        }
    }
}

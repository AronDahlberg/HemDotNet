using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    public class RealEstateAgentService : BaseHttpService, IRealEstateAgentService
    {
        private readonly IClient _client;

        public RealEstateAgentService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
            _client = client;
        }
        // Author: Allan
        public async Task<Response<List<PartialMarketPropertyDTO>>> GetAgent(string agentId)
        {
            Response<List<PartialMarketPropertyDTO>> response;

            try
            {

                var data = await _client.RealEstateAgentGETAsync("a");
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

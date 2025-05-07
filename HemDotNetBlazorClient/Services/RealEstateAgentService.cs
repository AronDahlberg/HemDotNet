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
        public async Task<Response<RealEstateAgentDto>> GetAgentByIdAsync(string agentId)
        {
            Response<RealEstateAgentDto> response;

            try
            {
                var data = await _client.GetProfileAsync(agentId);
                response = new Response<RealEstateAgentDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<RealEstateAgentDto>(ex);
            }

            return response;
        }

        public async Task<Response<string>> GetProfileImageUrl(string agentId)
        {
            Response<string> response;

            try
            {
                var data = await _client.GetProfileAsync(agentId);
                response = new Response<string>
                {
                    Data = data.RealEstateAgentImageUrl,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<string>(ex);
            }

            return response;
        }
    }
}

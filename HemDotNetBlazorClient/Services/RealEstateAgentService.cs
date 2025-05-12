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


        // Author: Chris
        public async Task<Response<IEnumerable<RealEstateAgentDto>>> GetAgentsAsync(string? municipality, string? firstName, string? lastName, string? agencyName, string? email, string? phoneNumber)
        {
            Response<IEnumerable<RealEstateAgentDto>> response;

            try
            {
                var data = await _client.RealEstateAgentAllAsync(municipality, firstName, lastName, agencyName, email, phoneNumber);
                response = new Response<IEnumerable<RealEstateAgentDto>>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<IEnumerable<RealEstateAgentDto>>(ex);
            }

            return response;
        }

        // Allan
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

        // Allan
        public async Task<Response<RealEstateAgentDto>> UpdateAgentAgencyAsync(string agentId, int newAgencyId)
        {
            Response<RealEstateAgentDto> response;

            try
            {
                
                // I was forgetting this... Since the endpoint used has authorization
                // for admin, we need this line to tell it we're logged in and admin
                await GetBearerToken();

                var data = await _client.UpdateAgencyAsync(agentId, newAgencyId);
                response = new Response<RealEstateAgentDto>
                {
                    Data = data,
                    Success = true,
                    Message = "Mäklarbyrå uppdaterad"
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<RealEstateAgentDto>(ex);
            }

            return response;
        }

        // Allan
        public async Task<Response<bool>> DeleteAgentAsync(string agentId)
        {
            try
            {
                await GetBearerToken();
                await _client.RealEstateAgentDELETEAsync(agentId);
                return new Response<bool> { Success = true, Data = true };
            }
            catch (ApiException ex) when (ex.StatusCode == 404)
            {
                Console.WriteLine($"Agent not found: {ex.Message}");
                return new Response<bool>
                {
                    Success = false,
                    Message = "Mäklaren kunde inte hittas.",
                    Data = false
                };
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"API error while deleting agent: {ex.Message}");
                return new Response<bool>
                {
                    Success = false,
                    Message = "Ett API-fel inträffade vid radering.",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return new Response<bool>
                {
                    Success = false,
                    Message = "Ett oväntat fel inträffade.",
                    Data = false
                };
            }
        }


    }
}

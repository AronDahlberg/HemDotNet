using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public class RealEstateAgencyService : BaseHttpService, IRealEstateAgencyService
    {
        private readonly IClient _client;

        public RealEstateAgencyService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
            _client = client;
        }

        // Author: Allan
        public async Task<Response<List<AgencyNameDto>>> GetAllAgencies()
        {
            Response<List<AgencyNameDto>> response;

            try
            {
                var data = await _client.RealEstateAgencyAsync();
                response = new Response<List<AgencyNameDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<AgencyNameDto>>(ex);
            }

            return response;
        }

        // Author: Allan
        public async Task<Response<List<AgencyDto>>> GetAllAgenciesPartial()
        {
            Response<List<AgencyDto>> response;

            try
            {
                var data = await _client.PartialAgenciesAsync();
                response = new Response<List<AgencyDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<AgencyDto>>(ex);
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAgency(int id)
        {
            Response<bool> response;

            try
            {
                await GetBearerToken();

                await _client.DeleteAsync(id);
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

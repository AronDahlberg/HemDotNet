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
    }
}

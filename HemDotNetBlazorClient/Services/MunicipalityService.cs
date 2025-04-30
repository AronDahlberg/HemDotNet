using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public class MunicipalityService : BaseHttpService, IMunicipalityService
    {
        private readonly IClient _client;

        public MunicipalityService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
            _client = client;
        }

        // Author: Allan
        public async Task<Response<List<MunicipalityNameDto>>> MunicipalitiesSearch(string searchTerm)
        {
            Response<List<MunicipalityNameDto>> response;

            try
            {
                await GetBearerToken();

                var data = await _client.SearchAsync(searchTerm);
                response = new Response<List<MunicipalityNameDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<MunicipalityNameDto>>(ex);
            }

            return response;
        }
    }
}

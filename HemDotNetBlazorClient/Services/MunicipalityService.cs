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
        public async Task<Response<List<MunicipalityDto>>> MunicipalitiesSearch(string searchTerm)
        {
            Response<List<MunicipalityDto>> response;

            try
            {
                await GetBearerToken();

                var data = await _client.SearchAsync(searchTerm);
                response = new Response<List<MunicipalityDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<MunicipalityDto>>(ex);
            }

            return response;
        }

        //Co-Author: Johan
        public async Task<Response<List<MunicipalityDto>>> GetAllMunicipalities()
        {
            Response<List<MunicipalityDto>> response;

            try
            {
                await GetBearerToken();

                var data = await _client.MunicipalityAsync();
                response = new Response<List<MunicipalityDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<MunicipalityDto>>(ex);
            }

            return response;
        }
    }
}

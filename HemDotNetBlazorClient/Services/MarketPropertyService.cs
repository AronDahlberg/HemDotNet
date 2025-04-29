using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    public class MarketPropertyService : BaseHttpService, IMarketPropertyService
    {
        private readonly IClient _client;

        public MarketPropertyService(ILocalStorageService localStorage, IClient client) : base(localStorage, client)
        {
            _client = client;
        }

        public async Task<Response<List<MarketProperty>>> GetCars()
        {
            Response<List<MarketProperty>> response;

            try
            {
                await GetBearerToken();
                var data = await _client.MarketPropertiesAsync();
                response = new Response<List<MarketProperty>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<List<Car>>(ex);
            }
            return response;
        }
    }
}

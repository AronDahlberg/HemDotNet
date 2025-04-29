using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    public class WeatherService : BaseHttpService
    {
        private readonly IClient _client;
        public WeatherService(ILocalStorageService localStorage, IClient client) : base(localStorage, client)
        {
            _client = client;
        }

        public async Task GetWeather()
        {
            try
            {
                //await GetBearerToken();
                var data = await _client.GetWeatherForecastAsync();

            }
            catch (Exception ex) { }

        }
    }
}

using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;
using System.Net.Http;
using System;

namespace HemDotNetBlazorClient.Services
{
    public class WeatherService : BaseHttpService
    {
        private readonly IClient _client;
        public WeatherService(ILocalStorageService localStorage, IClient client) : base(localStorage, client)
        {
            _client = client;
        }
        /*
        public async Task<WeatherForecast[]> GetWeather(string uri)
        {
                await GetBearerToken();
                //var data = await _client.GetWeatherForecastAsync();
                //return await _client.GetFromJsonAsync<WeatherForecast[]>(uri);


        }
        */
    }
}

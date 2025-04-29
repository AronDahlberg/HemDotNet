using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HemDotNetBlazorClient.Services
{
    public class AuthorizeApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthorizeApiClient(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<T?> GetAuthorizedJsonAsync<T>(string uri)
        {
            //var token = await _localStorage.GetItemAsync<string>("accessToken");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.GetFromJsonAsync<T>(uri);
        }
    }
}

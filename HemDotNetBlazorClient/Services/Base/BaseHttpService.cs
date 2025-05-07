using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;
using System.Net.Http.Headers;

//Coder: Johan, Participants: All

namespace HemDotNetBlazorClient.Services.Base
{
    public class BaseHttpService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IClient client;

        public BaseHttpService(ILocalStorageService localStorage, IClient client)
        {
            this.localStorage = localStorage;
            this.client = client;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
        {
            if (apiException.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Validation errors have occured.", ValidationErrors = apiException.Response, Success = false };
            }
            if (apiException.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "The requested item could not be found.", Success = false };
            }
            return new Response<Guid>() { Message = "Something went  wrong, please try again...", Success = false };
        }

        // Allan
        protected async Task GetBearerToken()
        {
            /*
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }*/

            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            
        }
    }
}
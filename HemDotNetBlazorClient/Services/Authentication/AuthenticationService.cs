using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;
using HemDotNetBlazorClient.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HemDotNetBlazorClient.Services.Authentication
{
    /* Coder: Adam, Participants: All */
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);

            // Store token
            await localStorage.SetItemAsync("accessToken", response.Token);

            // Change auth state of app, we need to make sure it's our ApiAuth provider beeing used.
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
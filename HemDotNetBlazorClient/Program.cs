using Blazored.LocalStorage;
using HemDotNetBlazorClient.Providers;
using HemDotNetBlazorClient.Services;
using HemDotNetBlazorClient.Services.Authentication;
using HemDotNetBlazorClient.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HemDotNetBlazorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Base HTTP client (NSwag and general use)
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7080/")
            });

            // Local storage for storing JWT tokens
            builder.Services.AddBlazoredLocalStorage();

            // Auth state management
            builder.Services.AddScoped<ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<ApiAuthenticationStateProvider>());
            builder.Services.AddAuthorizationCore();

            // NSwag client
            builder.Services.AddScoped<IClient, Client>();

            //  Application services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IMarketPropertyService, MarketPropertyService>();
            builder.Services.AddScoped<IMunicipalityService, MunicipalityService>();

            // Run the app
            await builder.Build().RunAsync();
        }
    }
}

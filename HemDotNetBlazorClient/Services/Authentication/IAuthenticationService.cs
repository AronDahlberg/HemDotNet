using HemDotNetBlazorClient.Services.Base;

namespace IdentityDemoClient.Services.Authentication
{
    /* Coder: Adam, Participants: All */
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);
        public Task Logout();
    }
}
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services.Authentication
{
    /* Coder: Adam, Participants: All */
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);
        public Task Logout();
    }
}
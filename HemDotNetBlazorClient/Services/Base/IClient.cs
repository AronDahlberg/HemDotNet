namespace HemDotNetBlazorClient.Services.Base
{
    public partial interface IClient
    {
        //Coder: Johan, Participants: Allan, Chris
        public HttpClient HttpClient { get; }

        public Task<T?> GetFromJsonAsync<T>(string uri);
    }
}

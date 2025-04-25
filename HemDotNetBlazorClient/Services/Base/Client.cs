namespace HemDotNetBlazorClient.Services.Base
{
    public partial class Client : IClient
    {
        //Coder: Johan, Participants: Allan, Chris
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }
    }
}

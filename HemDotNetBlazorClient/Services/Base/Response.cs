namespace HemDotNetBlazorClient.Services.Base
{
    public class Response<T>
    {
        //Coder: Johan, Participants: All

        public string Message { get; set; }
        public string ValidationErrors { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
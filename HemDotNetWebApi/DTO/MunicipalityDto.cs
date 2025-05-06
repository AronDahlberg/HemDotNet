namespace HemDotNetWebApi.DTO
{
    // Allan
    public class MunicipalityDto
    {
        public string Name { get; set; }

        //Co-author: Johan. Added Id to assist with linking a new MarketProperty to a Municipality client side.
        public int MunicipalityId { get; set; }
    }
}

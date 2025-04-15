using HemDotNetWebApi.Common;
using System.Text.Json.Serialization;

namespace HemDotNetWebApi.DTO
{
    // Allan
    public class MarketPropertyListingDto
    {
        public int MarketPropertyId { get; set; }
        public string PropertyAddress { get; set; }
        public string MunicipalityName { get; set; }
        public decimal Price { get; set; }
        public string PropertyImage { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PropertyCategory Category { get; set; }
    }
}

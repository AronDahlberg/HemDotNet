using HemDotNetWebApi.Common;
using HemDotNetWebApi.Models;
using System.Text.Json.Serialization;

namespace HemDotNetWebApi.DTO
{
    //Author: Johan Ek
    public class PartialMarketPropertyDTO
    {
        public string MunicipalityName { get; set; }
        public decimal Price { get; set; }
        public string PropertyAddress { get; set; }
        public virtual IEnumerable<PartialPropertyImageDTO> Images { get; set; }

        //Co-Author: Allan Crépin
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PropertyCategory Category { get; set; }
    }
}

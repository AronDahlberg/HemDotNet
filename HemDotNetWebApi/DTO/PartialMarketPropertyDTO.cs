using HemDotNetWebApi.Common;
using HemDotNetWebApi.Models;
using System.Text.Json.Serialization;

namespace HemDotNetWebApi.DTO
{
    //Author: Johan Ek
    // Co-author: Allan (Add some data to this)
    public class PartialMarketPropertyDTO
    {
        public int MarketPropertyId { get; set; }
        public string MunicipalityName { get; set; }
        public decimal Price { get; set; }
        public double LivingArea { get; set; }
        public double AncillaryArea { get; set; }
        public double LotArea { get; set; }
        public string Description { get; set; }
        public int AmountOfRooms { get; set; }
        public decimal MonthlyFee { get; set; }
        public string PropertyAddress { get; set; }
        public string RealEstateAgencyName { get; set; }
        public string RealEstateAgencyLogoUrl { get; set; }
        public virtual IEnumerable<PartialPropertyImageDTO> Images { get; set; }

        //Co-Author: Allan Crépin
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PropertyCategory Category { get; set; }
    }
}

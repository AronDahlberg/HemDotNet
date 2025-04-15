using HemDotNetWebApi.Common;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.DTOs
{
    // Chris: Minimal Data about Property (Active Market Listing)
    public class ActiveMarketListingDTO
    {
        public int MarketPropertyId { get; set; }
        public Municipality Municipality { get; set; }
        public PropertyCategory Category { get; set; }
        public virtual List<PropertyImage> Images { get; set; }
        public string PropertyAddress { get; set; }
        public decimal Price { get; set; }
    }
}

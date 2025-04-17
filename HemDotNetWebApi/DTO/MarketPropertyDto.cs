using HemDotNetWebApi.Common;

namespace HemDotNetWebApi.DTO
{
    public class MarketPropertyDto
    {
        public int MarketPropertyId { get; set; }
        public string MunicipalityName { get; set; }
        public int MunicipalityId { get; set; }
        public PropertyCategory Category { get; set; }
        public decimal Price { get; set; }
        public double LivingArea { get; set; }
        public double AncillaryArea { get; set; }
        public double LotArea { get; set; }
        public string PropertyAddress { get; set; }
        public string Description { get; set; }
        public int AmountOfRooms { get; set; }
        public decimal? MonthlyFee { get; set; }
        public decimal? YearlyMaintenanceCost { get; set; }
        public int ContructionYear { get; set; }
        public string RealEstateAgentName { get; set; }
        public int RealEstateAgentId { get; set; }
        public List<PropertyImageDto> Images { get; set; }
    }
}

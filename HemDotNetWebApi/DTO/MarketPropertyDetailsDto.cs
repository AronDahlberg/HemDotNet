using HemDotNetWebApi.Common;

namespace HemDotNetWebApi.DTO
{
    // Katarina
    public class MarketPropertyDetailsDto
    {
        public int MarketPropertyId { get; set; }
        public string MunicipalityName { get; set; }
        public string RealEstateAgentId { get; set; }
        public string RealEstateAgentFullName { get; set; }
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
        public int ConstructionYear { get; set; }
        public List<PropertyImageDto> Images { get; set; }
    }
}

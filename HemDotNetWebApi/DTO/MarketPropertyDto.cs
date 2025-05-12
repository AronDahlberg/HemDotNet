using HemDotNetWebApi.Common;

namespace HemDotNetWebApi.DTO
{
    // Allan
    public class MarketPropertyDto
    {
        public int MarketPropertyId { get; set; }
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
        public int ConstructionYear { get; set; }
        public int RealEstateAgentId { get; set; }

    }
}

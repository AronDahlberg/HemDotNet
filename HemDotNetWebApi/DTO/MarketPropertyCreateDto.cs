using HemDotNetWebApi.Common;
using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    // Katarina
    public class MarketPropertyCreateDto
    {
        [Required]
        public string RealEstateAgentId { get; set; }
        [Required]
        public int MunicipalityId { get; set; }
        [Required]
        public PropertyCategory Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public double LivingArea { get; set; }
        [Required]
        public double AncillaryArea { get; set; }
        [Required]
        public double LotArea { get; set; }
        [Required]
        public string PropertyAddress { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int AmountOfRooms { get; set; }
        [Required]
        public decimal MonthlyFee { get; set; }
        [Required]
        public decimal YearlyMaintenanceCost { get; set; }
        [Required]
        public int ConstructionYear { get; set; }
    }

}

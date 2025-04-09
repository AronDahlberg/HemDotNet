using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemDotNetWebApi.Models
{
    public class MarketProperty
    {
        [Key]
        public int MarketPropertyId { get; set; }

        [ForeignKey("MunicipalityId")]
        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }

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
        [MaxLength(200)]
        public string Description { get; set; }
        
        [Required]
        public int AmountOfRooms { get; set; }

        public decimal? MonthlyFee { get; set; }
        public decimal? YearlyMaintenanceCost { get; set; }

        [Required]
        public int ContructionYear { get; set; }

        [ForeignKey("RealEstateAgentId")]
        public string RealEstateAgentId { get; set; }
        public RealEstateAgent RealEstateAgent { get; set; }

        [NotMapped]
        public virtual List<PropertyImage> Images { get; set; }
    }
}

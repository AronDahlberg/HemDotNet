using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HemDotNetWebApi.Common;

namespace HemDotNetWebApi.Models
{
    public class MarketProperty
    {
        [Key]
        public int MarketPropertyId { get; set; }

        [Required]
        [ForeignKey("MunicipalityId")]
        public Municipality Municipality { get; set; }

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
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int AmountOfRooms { get; set; }

        [Required]
        public decimal? MonthlyFee { get; set; }
        [Required]
        public decimal? YearlyMaintenanceCost { get; set; }

        [Required]
        public int ContructionYear { get; set; }

        [Required]
        [ForeignKey("RealEstateAgentId")]
        public RealEstateAgent RealEstateAgent { get; set; }

        [Required]
        public virtual List<PropertyImage> Images { get; set; } = new();

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public bool IsSold { get; set; } = false;

    }
}

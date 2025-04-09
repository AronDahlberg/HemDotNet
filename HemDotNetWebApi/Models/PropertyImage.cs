using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemDotNetWebApi.Models
{
    public class PropertyImage
    {
        [Key]
        public int PropertyImageId { get; set; }

        [Required]
        [ForeignKey("MarketProperty")]
        public MarketProperty PropertyImageMarketProperty { get; set; }

        [Required]
        public string PropertyImageUrl { get; set; }

    }
}

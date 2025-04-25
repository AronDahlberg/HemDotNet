using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemDotNetWebApi.Models
{
    // Author: All
    public class PropertyImage
    {
        [Key]
        public int PropertyImageId { get; set; }

        //Co-Author: Johan Ek.
        //Updated the foreign key relationship to be more explicit.
        [Required]
        public int MarketPropertyId { get; set; }

        [ForeignKey("MarketPropertyId")]
        public MarketProperty MarketProperty { get; set; }

        [Required]
        public string PropertyImageUrl { get; set; }

    }
}

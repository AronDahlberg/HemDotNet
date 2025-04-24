using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemDotNetWebApi.Models
{
    // Author: All
    public class Municipality
    {
        [Key]
        public int MunicipalityId { get; set; }

        [Required]
        [MaxLength(30)]
        public string MunicipalityName { get; set; }

        [NotMapped]
        public virtual List<MarketProperty> MunicipalityProperties { get; set; }
    }
}

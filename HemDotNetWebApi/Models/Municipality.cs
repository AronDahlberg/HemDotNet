using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemDotNetWebApi.Models
{
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

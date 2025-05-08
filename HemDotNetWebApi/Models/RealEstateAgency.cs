using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HemDotNetWebApi.Models
{
    // Author: All
    public class RealEstateAgency
    {
        [Key]
        public int RealEstateAgencyId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RealEstateAgencyName { get; set; }

        [Required]
        public string RealEstateAgencyPresentation { get; set; }

        [Required]
        public string RealEstateAgencyLogoUrl { get; set; }

        [Required]
        public string RealEstateAgencyMunicipality { get; set; }

        [NotMapped]
        public virtual List<RealEstateAgent> RealEstateAgencyAgents { get; set; }
    }
}

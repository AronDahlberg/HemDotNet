using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemDotNetWebApi.Models
{
    // Author: All
    public class RealEstateAgent : IdentityUser
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RealEstateAgentId { get; set; }

        [Required]
        [MaxLength(30)]
        public string RealEstateAgentFirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string RealEstateAgentLastName { get; set; }

        [Required]
        public string RealEstateAgentEmail { get; set; }

        [Required]
        public string RealEstateAgentPhoneNumber { get; set; }

        [Required]
        public string RealEstateAgentImageUrl { get; set; }

        
        [ForeignKey("RealEstateAgency")]
        public RealEstateAgency? RealEstateAgentAgency { get; set; }

        //[NotMapped] Hope nothing breaks by commenting out
        public virtual List<MarketProperty> RealEstateAgentProperties { get; set; }
    }
}

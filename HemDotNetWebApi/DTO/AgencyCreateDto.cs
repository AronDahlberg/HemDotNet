using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    public class AgencyCreateDto
    {
        [MaxLength(100)]
        public string RealEstateAgencyName { get; set; }
        public string RealEstateAgencyPresentation { get; set; }
        public string RealEstateAgencyLogoUrl { get; set; }
        public string RealEstateAgencyMunicipality { get; set; }
    }
}

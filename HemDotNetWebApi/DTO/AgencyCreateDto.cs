using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    public class AgencyCreateDto
    {
        [MaxLength(100)]
        public string RealEstateAgencyName { get; set; } = string.Empty;
        public string RealEstateAgencyPresentation { get; set; } = string.Empty;
        public string RealEstateAgencyLogoUrl { get; set; } = string.Empty;
        public string RealEstateAgencyMunicipality { get; set; } = string.Empty;
    }
}

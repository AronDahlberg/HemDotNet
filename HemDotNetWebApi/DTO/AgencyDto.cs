using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    // Allan
    public class AgencyDto
    {
        public int RealEstateAgencyId { get; set; }
        public string RealEstateAgencyName { get; set; } = string.Empty;
        public string RealEstateAgencyPresentation { get; set; } = string.Empty;
        public string RealEstateAgencyLogoUrl { get; set; } = string.Empty;
        public string RealEstateAgencyMunicipality { get; set; } = string.Empty;
        public int NumberOfAgents { get; set; }
    }
}

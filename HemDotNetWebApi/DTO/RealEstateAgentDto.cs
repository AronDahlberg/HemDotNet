namespace HemDotNetWebApi.DTO
{
    // Allan
    public class RealEstateAgentDto
    {
        public string Id { get; set; }
        public string RealEstateAgentFirstName { get; set; }
        public string RealEstateAgentLastName { get; set; }
        public string RealEstateAgentEmail { get; set; }
        public string RealEstateAgentPhoneNumber { get; set; }
        public string RealEstateAgentImageUrl { get; set; }
        public string RealEstateAgentAgencyName { get; set; }
        public string RealEstateAgencyPresentation { get; set; }
        public string RealEstateAgencyLogoUrl { get; set; }
        public int RealEstateAgentAgencyId { get; set; }
        public List<int>? PropertyIds { get; set; }
    }
}

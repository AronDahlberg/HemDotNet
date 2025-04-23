
namespace HemDotNetWebApi.DTOs
{
    // Chris: Minimal Data for when updating RealEstateAgent
    public class RealEstateAgentUpdateDTO
    {
        //public int RealEstateAgentId { get; set; }
        public string RealEstateAgentFirstName { get; set; }
        public string RealEstateAgentLastName { get; set; }
        public string RealEstateAgentEmail { get; set; }
        public string RealEstateAgentPhoneNumber { get; set; }    
        public string RealEstateAgentImageUrl { get; set; }
    }
}

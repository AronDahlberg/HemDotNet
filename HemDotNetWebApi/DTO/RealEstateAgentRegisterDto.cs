using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    /* Coder: Allan, Participants: All */
    public class RealEstateAgentRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int RealEstateAgencyId { get; set; }

    }
}
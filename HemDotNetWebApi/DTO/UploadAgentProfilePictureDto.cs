using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    public class UploadAgentProfilePictureDto
    {
        [Required]
        public string AgentId { get; set; }
    }
}

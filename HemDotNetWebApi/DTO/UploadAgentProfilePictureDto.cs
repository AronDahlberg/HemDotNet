using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    // Allan
    public class UploadAgentProfilePictureDto
    {
        [Required]
        public string AgentId { get; set; }
    }
}

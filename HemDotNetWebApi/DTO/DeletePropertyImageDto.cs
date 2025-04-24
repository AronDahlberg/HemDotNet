using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    // Allan
    public class DeletePropertyImageDto
    {
        [Required]
        public int PropertyImageId { get; set; }
    }
}

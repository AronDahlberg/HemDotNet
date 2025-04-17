using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    public class DeletePropertyImageDto
    {
        [Required]
        public int PropertyImageId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    public class AddPropertyImageDto
    {
        [Required]
        public int MarketPropertyId { get; set; }
    }
}

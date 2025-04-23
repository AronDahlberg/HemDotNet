using System.ComponentModel.DataAnnotations;

namespace HemDotNetWebApi.DTO
{
    // Allan
    public class AddPropertyImageDto
    {
        [Required]
        public int MarketPropertyId { get; set; }
    }
}

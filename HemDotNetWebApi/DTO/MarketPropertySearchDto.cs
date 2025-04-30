using HemDotNetWebApi.Common;

namespace HemDotNetWebApi.DTO
{
    // Allan
    public class MarketPropertySearchDto
    {
        public string? Area { get; set; }
        public List<PropertyCategory>? SelectedTypes { get; set; } = new();
        public int? MinRooms { get; set; }
        public double? MinArea { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? NewProduction { get; set; }
    }
}

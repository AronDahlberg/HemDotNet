using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Data
{
    public class SearchModel
    {
        public string Area { get; set; } = "";
        public HashSet<PropertyCategory> SelectedTypes { get; set; } = new();
        public bool ShowAllTypes { get; set; } = true;
        public string MinRooms { get; set; } = "";
        public string MinArea { get; set; } = "";
        public string MaxPrice { get; set; } = "";
        public string NewProduction { get; set; } = "true";
    }
}

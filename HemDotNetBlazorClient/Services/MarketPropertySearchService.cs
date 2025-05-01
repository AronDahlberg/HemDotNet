using System.Collections.Specialized;
using System.Web;
using System.Text.Json;
using HemDotNetBlazorClient.Services.Base;
using HemDotNetBlazorClient.Data;

namespace HemDotNetBlazorClient.Services
{
    public static class MarketPropertySearchService
    {
        public static string ToQueryString(this SearchModel model)
        {
            var queryParams = new NameValueCollection();


            if (!string.IsNullOrEmpty(model.Area))
                queryParams.Add("area", model.Area);

            if (!string.IsNullOrEmpty(model.MinRooms))
                queryParams.Add("minRooms", model.MinRooms);

            if (!string.IsNullOrEmpty(model.MinArea))
                queryParams.Add("minArea", model.MinArea);

            if (!string.IsNullOrEmpty(model.MaxPrice))
                queryParams.Add("maxPrice", model.MaxPrice);

            queryParams.Add("showAllTypes", model.ShowAllTypes.ToString().ToLower());
            queryParams.Add("newProduction", model.NewProduction);

            if (model.SelectedTypes.Count > 0)
            {
                var selectedTypesJson = JsonSerializer.Serialize(model.SelectedTypes);
                queryParams.Add("selectedTypes", HttpUtility.UrlEncode(selectedTypesJson));
            }

            var queryString = new System.Text.StringBuilder();
            queryString.Append("?");

            foreach (string key in queryParams.Keys)
            {
                foreach (string value in queryParams.GetValues(key))
                {
                    queryString.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), value);
                }
            }

            if (queryString.Length > 0 && queryString[queryString.Length - 1] == '&')
                queryString.Length--;

            return queryString.ToString();
        }

        public static SearchModel ParseQueryString(string query)
        {
            var model = new SearchModel();
            var queryString = HttpUtility.ParseQueryString(query);

            model.Area = queryString["area"] ?? "";
            model.MinRooms = queryString["minRooms"] ?? "";
            model.MinArea = queryString["minArea"] ?? "";
            model.MaxPrice = queryString["maxPrice"] ?? "";

            if (bool.TryParse(queryString["showAllTypes"], out bool showAllTypes))
                model.ShowAllTypes = showAllTypes;

            model.NewProduction = queryString["newProduction"] ?? "true";

            var selectedTypesJson = queryString["selectedTypes"];
            if (!string.IsNullOrEmpty(selectedTypesJson))
            {
                try
                {
                    var selectedTypes = JsonSerializer.Deserialize<HashSet<PropertyCategory>>(
                        HttpUtility.UrlDecode(selectedTypesJson));
                    if (selectedTypes != null)
                        model.SelectedTypes = selectedTypes;
                }
                catch
                {
                    // Handle deserialization error if needed
                }
            }

            return model;
        }
    }
}

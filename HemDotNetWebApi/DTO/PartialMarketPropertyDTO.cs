using HemDotNetWebApi.Common;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.DTO
{
    //Author: Johan Ek
    public class PartialMarketPropertyDTO
    {
        public PropertyCategory Category { get; set; }
        public Municipality Municipality { get; set; }
        public decimal Price { get; set; }
        public string PropertyAddress { get; set; }
        public List<PropertyImage> Images { get; set; }

    }
}

using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IMarketPropertyRepository
    {
        //Author: Johan Ek
        Task<IEnumerable<MarketProperty>> GetAllMarketProperties();
    }
}

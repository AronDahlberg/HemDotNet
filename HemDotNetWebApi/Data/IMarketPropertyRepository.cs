using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;

namespace HemDotNetWebApi.Data
{
    public interface IMarketPropertyRepository
    {
        //Author: Johan Ek
        Task<IEnumerable<PartialMarketPropertyDTO>> GetAllMarketPropertiesPartial();
    }
}

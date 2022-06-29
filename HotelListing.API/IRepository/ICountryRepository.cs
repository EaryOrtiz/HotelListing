using HotelListing.API.Data;

namespace HotelListing.API.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int? id);
    }
}

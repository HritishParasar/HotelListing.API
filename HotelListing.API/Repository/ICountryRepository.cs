using HotelListing.API.Models;

namespace HotelListing.API.Repository
{
    public interface ICountryRepository
    {
        Task<Country> GetCountryByID(int id);
        Task<List<Country>> GetAllCountry();
        Task AddCountry(Country country);
        Task UpdateCountry(int id,Country country);
        Task DeleteCountry(int id);
    }
}

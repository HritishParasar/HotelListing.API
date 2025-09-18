using HotelListing.API.Models;

namespace HotelListing.API.Repository
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotelByID(int id);
        Task<List<Hotel>> GetAllHotel();
        Task AddHotel(Hotel hotel);
        Task UpdateHotel(Hotel hotel);
        Task DeleteHotel(Hotel hotel);
    }
}

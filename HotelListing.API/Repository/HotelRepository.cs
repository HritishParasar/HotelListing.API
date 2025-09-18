using HotelListing.API.Data;
using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext dbContext;

        public HotelRepository(HotelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddHotel(Hotel hotel)
        {
            await dbContext.Hotels.AddAsync(hotel);
            await dbContext.SaveChangesAsync();
        }


        public async Task DeleteHotel(Hotel hotel)
        {
            dbContext.Hotels.Remove(hotel);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Hotel>> GetAllHotel()
        {
            var hotels = await dbContext.Hotels.ToListAsync();
            return hotels;
        }

        public async Task<Hotel> GetHotelByID(int id)
        {
            var hotel = await dbContext.Hotels.FirstOrDefaultAsync(x => x.Id == id);
            return hotel;
        }

        public async Task UpdateHotel(Hotel hotel)
        {
            dbContext.Hotels.Update(hotel);
            await dbContext.SaveChangesAsync();
        }
    }
}

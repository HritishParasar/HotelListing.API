using HotelListing.API.Data;
using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly HotelDbContext dbContext;

        public CountryRepository(HotelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddCountry(Country country)
        {
            await dbContext.Countries.AddAsync(country);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCountry(int id)
        {
            var find = await dbContext.Countries.FindAsync(id);
            dbContext.Countries.Remove(find);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Country>> GetAllCountry()
        {
            var countries = await dbContext.Countries.ToListAsync();
            return countries;
        }

        public async Task<Country> GetCountryByID(int id)
        {
            var country = await dbContext.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id);
            return country;
        }

        public async Task UpdateCountry(int id, Country country)
        {
            var find = await dbContext.Countries.FindAsync(id);
            find.Name = country.Name;
            find.ShortName = country.ShortName;
            dbContext.Countries.Update(find);
            await dbContext.SaveChangesAsync();
        }
    }
}

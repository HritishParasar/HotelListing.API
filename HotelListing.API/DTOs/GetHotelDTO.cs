using HotelListing.API.Models;

namespace HotelListing.API.DTOs
{
    public class GetHotelDTO
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}

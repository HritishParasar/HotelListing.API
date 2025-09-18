using HotelListing.API.Models;

namespace HotelListing.API.DTOs
{
    public class GetCountryDTO
    {
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public IList<Hotel>? Hotels { get; set; }
    }
}

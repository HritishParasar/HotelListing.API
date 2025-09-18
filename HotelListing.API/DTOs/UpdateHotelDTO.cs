namespace HotelListing.API.DTOs
{
    public class UpdateHotelDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double Rating { get; set; }
    }
}

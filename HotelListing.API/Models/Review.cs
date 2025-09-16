namespace HotelListing.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? ReviewerName { get; set; }
        public string? ReviewText { get; set; }
        public double Rating { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }   
    }
}

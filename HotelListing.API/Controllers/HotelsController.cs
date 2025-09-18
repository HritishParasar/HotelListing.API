using HotelListing.API.DTOs;
using HotelListing.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository repository;

        public HotelsController(IHotelRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("getAllHotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            try
            {
                var hotels = await repository.GetAllHotel();
                if(hotels == null)
                {
                    return NotFound("No Hotels Found.");
                }
                var hotelsDTO = hotels.Select(h => new GetHotelDTO
                {
                    Name = h.Name,
                    Address = h.Address,
                    Rating = h.Rating,
                    CountryId = h.CountryId,
                    Country = h.Country
                });
                return Ok(hotelsDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getHotelById/{id}")]
        public async Task<IActionResult> GetHotelByID(int id)
        {
            try
            {
                var hotel = await repository.GetHotelByID(id);
                if(hotel == null)
                {
                    return NotFound($"No Hotel Found with ID : {id}");
                }
                var hotelDTO = new GetHotelDTO
                {
                    Name = hotel.Name,
                    Address = hotel.Address,
                    Rating = hotel.Rating,
                    CountryId = hotel.CountryId,
                    Country = hotel.Country
                };
                return Ok(hotelDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("addHotel")]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelDTO hotel)
        {
            try
            {
                if(hotel == null)
                {
                    return BadRequest("Hotel is null.");
                }
                var newHotel = new Models.Hotel
                {
                    Name = hotel.Name,
                    Address = hotel.Address,
                    Rating = hotel.Rating,
                    CountryId = hotel.CountryId
                };
                await repository.AddHotel(newHotel);
                return Ok("Hotel Added Successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updateHotel/{id}")]
        public async Task<IActionResult> UpdateHotel(int id,[FromBody] UpdateHotelDTO hotel)
        {
            try
            {
                if(id != hotel.Id)
                {
                    return BadRequest("Given ID does not match with Hotel Id");
                }
                var find = await repository.GetHotelByID(id);
                if(find == null)
                {
                    return BadRequest("Hotel is null.");
                }
                find.Name = hotel.Name;
                find.Address = hotel.Address;
                find.Rating = hotel.Rating;
                await repository.UpdateHotel(find);
                return Ok("Hotel Updated Successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("deletehotel")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            try
            {
                var find = await repository.GetHotelByID(id);
                if(find == null)
                {
                    return NotFound($"No Hotel Found with ID : {id}");
                }
                await repository.DeleteHotel(find);
                return Ok("Hotel Deleted Successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

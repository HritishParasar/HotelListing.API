using HotelListing.API.DTOs;
using HotelListing.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _repository;
        public CountriesController(ICountryRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("getAllCountry")]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _repository.GetAllCountry();
                if (countries == null || !countries.Any())
                {
                    return NotFound("No countries found.");
                }
                var countriesDto = countries.Select(c => new GetCountryDTO
                {
                    Name = c.Name,
                    ShortName = c.ShortName,
                    Hotels = c.Hotels
                }).ToList();
                return Ok(countriesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("getCountryById/{id:int}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            try
            {
                var country = await _repository.GetCountryByID(id);
                if (country == null)
                {
                    return NotFound($"Country with ID {id} not found.");
                }
                var countryDTO = new GetCountryDTO
                {
                    Name = country.Name,
                    ShortName = country.ShortName,
                    Hotels = country.Hotels
                };
                return Ok(countryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("addCountry")]
        public async Task<IActionResult> AddCountry([FromBody] AddCountry country)
        {
            try
            {
                if (country == null)
                {
                    return BadRequest("Country data is null.");
                }
                var newCountry = new Models.Country
                {
                    Id = new int(), // Assuming ID is auto-generated
                    Name = country.Name,
                    ShortName = country.ShortName
                };
                await _repository.AddCountry(newCountry);
                return CreatedAtAction(nameof(GetCountryById), new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("updateCountry/{id:int}")]
        public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDTO country)
        {
            try
            {
                if (country == null || id != country.Id)
                {
                    return BadRequest("Invalid country data.");
                }
                var existingCountry = await _repository.GetCountryByID(id);
                if (existingCountry == null)
                {
                    return NotFound($"Country with ID {id} not found.");
                }
                var updatedCountry = new Models.Country
                {
                    Id = country.Id,
                    Name = country.Name,
                    ShortName = country.ShortName
                };
                await _repository.UpdateCountry(id, updatedCountry);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("deleteCountry/{id:int}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                var existingCountry = await _repository.GetCountryByID(id);
                if (existingCountry == null)
                {
                    return NotFound($"Country with ID {id} not found.");
                }
                await _repository.DeleteCountry(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

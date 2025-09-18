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
                return Ok(countries);
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
                return Ok(country);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("addCountry")]
        public async Task<IActionResult> AddCountry([FromBody] Models.Country country)
        {
            try
            {
                if (country == null)
                {
                    return BadRequest("Country data is null.");
                }
                await _repository.AddCountry(country);
                return CreatedAtAction(nameof(GetCountryById), new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("updateCountry/{id:int}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] Models.Country country)
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
                await _repository.UpdateCountry(id, country);
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

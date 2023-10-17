using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Models.Service;
using WeatherForecast.Api.Services;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _countryService.GetAllAsync();
            return Ok(countries);
        }

        [HttpGet("countries/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var countries = await _countryService.GetByIdAsync(id);
            if (countries == null)
            {
                return NotFound();
            }

            return Ok(countries);
        }

        [HttpPost("countries")]
        public async Task<IActionResult> Create([FromBody] Country countryBody)
        {
            var country = new Country
            {
                Name = countryBody.Name,
                CountryCode = countryBody.CountryCode
            };

            var created = await _countryService.CreateAsync(country);
            if (!created)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = country.Id }, country);
        }

        [HttpDelete("countries/{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deleted = await _countryService.DeleteByIdAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
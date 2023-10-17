using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Models.Service;
using WeatherForecast.Api.Services;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController: ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _cityService.GetAllAsync();
            return Ok(cities);
        }

        [HttpGet("citiesByCountryName/{countryName}")]
        public async Task<IActionResult> GetAllByCountryName(string countryName)
        {
            var cities = await _cityService.GetAllByCountryNameAsync(countryName);
            return Ok(cities);
        }

        [HttpGet("cities/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cities = await _cityService.GetByIdAsync(id);
            if (cities == null)
            {
                return NotFound();
            }

            return Ok(cities);
        }

        [HttpPost("cities")]
        public async Task<IActionResult> Create([FromBody] City cityBody)
        {
            var city = new City
            {
                Name = cityBody.Name,
                CountryId = cityBody.CountryId
            };

            var created = await _cityService.CreateAsync(city);
            if (!created)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = city.Id }, city);
        }

        [HttpDelete("cities/{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deleted = await _cityService.DeleteByIdAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

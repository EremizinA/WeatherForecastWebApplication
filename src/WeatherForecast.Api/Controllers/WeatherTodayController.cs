using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Services;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherTodayController : ControllerBase
    {
        private readonly IWeatherTodayService _weatherTodayService;
        public WeatherTodayController(IWeatherTodayService weatherTodayService)
        {
            _weatherTodayService = weatherTodayService;
        }

        [HttpGet("GetWeatherForToday")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _weatherTodayService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("CreateWeatherForToday")]
        public async Task<IActionResult> CreateAll([FromBody] IEnumerable<WeatherToday> weatherTodayList)
        {
            var created = await _weatherTodayService.CreateAllAsync(weatherTodayList);
            if (!created)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateAll), 
                new WeatherIdsList
                {
                    WeatherIds = weatherTodayList.Select(w => w.Id).ToList()
                });
        }

        [HttpDelete("DeleteWeatherForToday")]
        public async Task<IActionResult> DeleteAll()
        {
            var deleted = await _weatherTodayService.DeleteAllAsync();
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

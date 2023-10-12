using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherDataController : ControllerBase
    {
        public WeatherDataController()
        {
        }

        [HttpGet("GetWeatherDataFromApi")]
        public async Task<IActionResult> GetAll()
        {
            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync("http://localhost:51010/WeatherForecast/GetAllWeatherForecastForToday");
            var content = await request.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<List<WeatherToday>>(content);
            return Ok(countries);
        }
    }
}

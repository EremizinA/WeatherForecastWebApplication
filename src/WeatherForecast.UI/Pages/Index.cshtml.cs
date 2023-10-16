using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WeatherForecast.Api.Controllers;
using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WeatherTodayController _weatherTodayController;
        private readonly ILogger<IndexModel> _logger;
        
        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public List<WeatherTodayFrontEnd> WeatherTodayFrontEndList { get; set; } = new List<WeatherTodayFrontEnd>();

        public IndexModel(WeatherTodayController weatherTodayController, ILogger<IndexModel> logger)
        {
            _weatherTodayController = weatherTodayController;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostGetWeatherToday()
        {
            var result = await _weatherTodayController.GetAllForFrontEnd();
            if (result is OkObjectResult okResult)
            {
                if (okResult.Value is IEnumerable<WeatherTodayFrontEnd> weatherList)
                {
                    WeatherTodayFrontEndList = weatherList.ToList();
                }
            }
            return Page();
        }

        public IActionResult OnPostClearWeather()
        {
            WeatherTodayFrontEndList = new List<WeatherTodayFrontEnd>();
            return Page();
        }
    }
}
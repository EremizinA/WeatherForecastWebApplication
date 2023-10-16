using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherForecast.Api.Controllers;
using WeatherForecast.Api.Models;

namespace WeatherForecast.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WeatherTodayController _weatherTodayController;
        private readonly ILogger<IndexModel> _logger;
        
        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public List<WeatherToday> WeatherTodayList { get; set; }

        public IndexModel(WeatherTodayController weatherTodayController, ILogger<IndexModel> logger)
        {
            _weatherTodayController = weatherTodayController;
            _logger = logger;
        }

        public async Task OnGet()
        {
            var result = await _weatherTodayController.GetAll();
            var okResult = result as OkObjectResult;
            if (okResult != null)
            {
                var weatherList = okResult.Value as List<WeatherToday>;
                if(weatherList != null)
                {
                    WeatherTodayList = weatherList;
                }
            }
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using WeatherForecast.Api.Controllers;
using WeatherForecast.Api.Models.Service;
using static System.Net.Mime.MediaTypeNames;

namespace WeatherForecast.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WeatherTodayController _weatherTodayController;
        private readonly CountryController _countryController;
        private readonly CityController _cityController;
        private readonly ILogger<IndexModel> _logger;
        
        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        public static SelectList? Countries { get; set; }

        public static SelectList? Cities { get; set; }

        [BindProperty]
        public List<WeatherTodayFrontEnd> WeatherTodayFrontEndList { get; set; } = new List<WeatherTodayFrontEnd>();

        [BindProperty]
        public static string SelectedCountry { get; set; } = string.Empty;

        [BindProperty]
        public static string SelectedCity { get; set; } = string.Empty;

        public IndexModel(
            WeatherTodayController weatherTodayController, 
            ILogger<IndexModel> logger,
            CountryController countryController,
            CityController cityController)
        {
            _weatherTodayController = weatherTodayController;
            _logger = logger;
            _countryController = countryController;
            _cityController = cityController;
        }

        public async Task OnGet()
        {
            var countryResult = await _countryController.GetAll();
            var countriesList = new List<Country>();

            if (countryResult is OkObjectResult okCountryResult)
            {
                if (okCountryResult.Value is IEnumerable<Country> countryList)
                {
                    countriesList = countryList.ToList();
                }
            }

            var countrySelectListItems = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Select country...", Value = "-1" }
            };
            foreach (var country in countriesList)
            {
                countrySelectListItems.Add(new SelectListItem { Selected = false, Text = country.Name, Value = country.Id.ToString() });
            }

            Countries = new SelectList(countrySelectListItems, "Value", "Text", -1);    
        }

        public async Task OnSelectSelectedCountry()
        {
            if (!SelectedCountry.IsNullOrEmpty())
            {
                var cityResult = await _cityController.GetAllByCountryName(SelectedCountry);
                var citiesList = new List<City>();


                if (cityResult is OkObjectResult okCityResult)
                {
                    if (okCityResult.Value is IEnumerable<City> cityList)
                    {
                        citiesList = cityList.ToList();
                    }
                }

                var citySelectListItems = new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Select city...", Value = "-1" }
                };
                foreach (var city in citiesList)
                {
                    citySelectListItems.Add(new SelectListItem { Selected = false, Text = city.Name, Value = city.Id.ToString() });
                }

                Cities = new SelectList(citySelectListItems, "Value", "Text", -1);
            }
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

        //public async Task<IActionResult> OnPostGetAllCountries()
        //{
        //    var result = await _countryController.GetAll();
        //    if (result is OkObjectResult okResult)
        //    {
        //        if (okResult.Value is IEnumerable<Country> countriesList)
        //        {
        //            CountriesList = countriesList.ToList();
        //        }
        //    }
        //    return Page();
        //}

        //public async Task<IActionResult> OnPostGetAllCities()
        //{
        //    var result = await _cityController.GetAll();
        //    if (result is OkObjectResult okResult)
        //    {
        //        if (okResult.Value is IEnumerable<City> citiesList)
        //        {
        //            CitiesList = citiesList.ToList();
        //        }
        //    }
        //    return Page();
        //}
    }
}
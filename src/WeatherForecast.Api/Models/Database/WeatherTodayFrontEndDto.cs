using Microsoft.Data.Sql;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Api.Models.Database
{
    public class WeatherTodayFrontEndDto
    {
        public string CityName { get; set; }

        public int Temperature { get; set; }

        public string Scale { get; set; }

        public string CountryName { get; set; }
    }
}

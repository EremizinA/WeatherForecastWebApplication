namespace WeatherForecast.Api.Models
{
    public class WeatherToday
    {
        public int Id { get; set; }

        public int Temperature { get; set; }

        public string Scale { get; set; }

        public int CityId { get; set; }
    }
}

namespace WeatherForecast.Api.Models.Service
{
    public class WeatherToday
    {
        public int Id { get; set; }

        public int Temperature { get; set; }

        public string Scale { get; set; }

        public int CityId { get; set; }
    }
}

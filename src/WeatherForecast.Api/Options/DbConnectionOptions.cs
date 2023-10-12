namespace WeatherForecast.Api.Options
{
    public class DbConnectionOptions
    {
        public string ConnectionString { get; init; } = default!;
        public string DatabaseName { get; init; } = default!;
    }
}

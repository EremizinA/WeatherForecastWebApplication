namespace WeatherForecast.Api.Services
{
    public interface IKafkaConsumerService
    {
        Task StartConsumeAsync();
    }
}

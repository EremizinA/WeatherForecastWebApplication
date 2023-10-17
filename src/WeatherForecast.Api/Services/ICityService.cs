using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Services
{
    public interface ICityService: IBaseService<City>
    {
        Task<IEnumerable<City?>> GetAllByCountryNameAsync(string countryName);
    }
}

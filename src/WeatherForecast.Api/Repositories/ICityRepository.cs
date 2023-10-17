using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Repositories
{
    public interface ICityRepository: IBaseRepository<City?>
    {
        Task<IEnumerable<City?>> GetAllByCountryNameAsync(string countryName);
    }
}

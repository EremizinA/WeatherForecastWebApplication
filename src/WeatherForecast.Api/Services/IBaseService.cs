using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Services
{
    public interface IBaseService<TEntity>
    {
        Task<IEnumerable<TEntity?>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TEntity? entity);
        Task<bool> DeleteByIdAsync(int id);
    }
}

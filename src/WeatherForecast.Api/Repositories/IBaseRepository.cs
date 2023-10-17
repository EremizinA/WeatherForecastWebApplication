namespace WeatherForecast.Api.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity?>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TEntity? entity);
        Task<bool> DeleteByIdAsync(int id);
    }
}

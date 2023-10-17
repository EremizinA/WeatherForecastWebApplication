using Dapper;
using WeatherForecast.Api.Data;
using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ISqlServerDbConnectionFactory _connectionFactory;

        public CityRepository(ISqlServerDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<City?>> GetAllAsync()
        {
            const string query = "select * from Cities";
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            return await connection.QueryAsync<City?>(query);
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            const string query = "select * from Cities where ID = @Id";
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            return await connection.QuerySingleOrDefaultAsync<City?>(query, new { Id = id });
        }

        public async Task<IEnumerable<City?>> GetAllByCountryNameAsync(string countryName)
        {
            var countryString = $"%{countryName}%";
            const string query = $"select Cities.ID, Cities.[Name], Cities.CountryID from Cities\r\njoin Countries\r\non Cities.CountryID = Countries.ID\r\nwhere Countries.[Name] like Formatmessage('%s', @CountryString)";
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            return await connection.QueryAsync<City?>(query, new { CountryString = countryString });
        }

        public async Task<bool> CreateAsync(City? city)
        {
            const string query = "INSERT INTO Cities ([Name],[CountryId]) VALUES (@Name, @CountryId)";
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            var result = await connection.ExecuteAsync(query, new { city?.Name, city?.CountryId });
            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            const string query = "DELETE FROM Cities where ID = @Id";
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}

using WeatherForecast.Api.Data;
using Dapper;
using WeatherForecast.Api.Models.Service;

namespace WeatherForecast.Api.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly ISqlServerDbConnectionFactory _connectionFactory;

    public CountryRepository(ISqlServerDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Country?>> GetAllAsync()
    {
        const string query = "select * from Countries";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<Country?>(query);
    }

    //TODO: need to put it in separate repository in future
    public async Task<IEnumerable<WeatherToday>> GetAllWeatherAsync()
    {
        const string query = "select * from WeatherToday";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<WeatherToday>(query);
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        const string query = "select * from Countries where ID = @Id";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Country?>(query, new { Id = id });
    }

    public async Task<bool> CreateAsync(Country? country)
    {
        const string query = "INSERT INTO Countries ([Name],[CountryCode]) VALUES (@Name, @CountryCode)";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(query, new { country?.Name, country?.CountryCode });
        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        const string query = "DELETE FROM Countries where ID = @Id";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(query, new { Id = id });
        return result > 0;
    }
}

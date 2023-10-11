using WeatherForecast.Api.Data;
using Dapper;

namespace WeatherForecast.Tests.Common.Services
{
    public class MsSqlClient
    {
        private readonly ISqlServerDbConnectionFactory _connectionFactory;

        public MsSqlClient(ISqlServerDbConnectionFactory connectionFactory) 
        { 
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<T>> Select<T>(string query)
        {
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            return await connection.QueryAsync<T>(query);
        }

        public async Task ExecuteSqlScript(string path)
        {
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            using var reader = new StreamReader(path);
            var sql = await reader.ReadToEndAsync();
            await connection.ExecuteAsync(sql);
        }

        public async Task ExecuteScript(string query)
        {
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            await connection.ExecuteAsync(query);
        }
    }
}

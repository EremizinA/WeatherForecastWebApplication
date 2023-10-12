using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WeatherForecast.Api.Options;

namespace WeatherForecast.Api.Data;

public class SqlServerDbConnectionFactory : ISqlServerDbConnectionFactory
{
    private readonly DbConnectionOptions _connectionOptions;

    public SqlServerDbConnectionFactory(IOptions<DbConnectionOptions> connectionOptions)
    {
        _connectionOptions = connectionOptions.Value;
    }

    public async Task<IDbConnection> CreateDbConnectionAsync()
    {
        var connection = new SqlConnection(_connectionOptions.ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
}

using System.Data;

namespace WeatherForecast.Api.Data;

public interface ISqlServerDbConnectionFactory
{
    Task<IDbConnection> CreateDbConnectionAsync();
}

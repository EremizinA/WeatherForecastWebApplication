﻿using WeatherForecast.Api.Data;
using Dapper;
using WeatherForecast.Api.Models.Service;
using WeatherForecast.Api.Models.Database;

namespace WeatherForecast.Api.Repositories;

public class WeatherTodayRepository : IWeatherTodayRepository
{
    private readonly ISqlServerDbConnectionFactory _connectionFactory;

    public WeatherTodayRepository(ISqlServerDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<WeatherToday>> GetAllAsync()
    {
        const string query = "select * from WeatherToday";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<WeatherToday>(query);
    }

    public async Task<IEnumerable<WeatherTodayFrontEndDto>> GetAllFrontEndDtoAsync()
    {
        const string query = "select Ct.[Name] as CityName, W.Temperature, W.Scale, Cn.[Name] as CountryName\r\nfrom WeatherToday as W\r\njoin Cities as Ct\r\non W.CityID = Ct.ID\r\njoin Countries as Cn\r\non Ct.CountryID = Cn.ID";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<WeatherTodayFrontEndDto>(query);
    }

    public async Task<bool> CreateAllAsync(IEnumerable<WeatherToday> weatherTodayList)
    {
        var query = "insert into WeatherToday ([Temperature],[Scale],[CityId]) values ";
        foreach(var weatherToday in weatherTodayList)
        {
            query += $"({weatherToday.Temperature}, '{weatherToday.Scale}', {weatherToday.CityId}), ";
        }
        var insertQuery = query.Remove(query.Length - 2);
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(insertQuery);
        return result > 0;
    }

    public async Task<bool> DeleteAllAsync()
    {
        const string query = "delete from WeatherToday";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(query);
        await ReseedIdentityAsync();
        return result > 0;
    }

    private async Task<bool> ReseedIdentityAsync()
    {
        const string query = "DBCC CHECKIDENT ('[WeatherToday]', RESEED, 0);";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(query);
        return result > 0;
    }
}


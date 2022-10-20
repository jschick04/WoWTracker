﻿using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Tracker.Api.Library.Database;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _configuration;

    public SqlDataAccess(IConfiguration configuration) => _configuration = configuration;

    public string ConnectionStringName { get; set; } = "TrackerDb";

    public async Task<List<T>> LoadData<T, TU>(string storedProcedure, TU parameters)
    {
        string connectionString = GetConnectionString();

        using IDbConnection db = new SqlConnection(connectionString);

        IEnumerable<T> data = await db.QueryAsync<T>(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return data.ToList();
    }

    public async Task SaveData<T>(string storedProcedure, T parameters)
    {
        string connectionString = GetConnectionString();

        using IDbConnection db = new SqlConnection(connectionString);

        await db.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    private string GetConnectionString()
    {
        var builder = new SqlConnectionStringBuilder(_configuration.GetConnectionString(ConnectionStringName))
        {
            ApplicationName = "WoW Tracker API Library"
        };

        return builder.ConnectionString;
    }
}

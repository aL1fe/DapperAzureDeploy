﻿using System.Data;
using Microsoft.Data.SqlClient;

namespace DapperLearning.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("AzureConnection");
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
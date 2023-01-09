﻿using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PhotoSpeech.DataAccess
{
    public class SqlDataAccess
    {
        private readonly string connStr;
        public SqlDataAccess()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            connStr = configuration.GetValue<string>("ConnectionStrings:photoSpeachDb");
        }
        public async Task<List<T>> LoadData<T>(string sql)
        {
            string connectionString = connStr;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql);
                return data.ToList();
            }
        }

        public async Task SaveData(string sql)
        {
            string connectionString = connStr;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}

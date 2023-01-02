using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PhotoSpeech.DataAccess
{
    public class SqlDataAccess
    {
        public async Task<List<T>> LoadData<T>(string sql)
        {
            string connectionString = "TODO";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql);
                return data.ToList();
            }
        }

        public async Task SaveData(string sql)
        {
            string connectionString = "TODO";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}

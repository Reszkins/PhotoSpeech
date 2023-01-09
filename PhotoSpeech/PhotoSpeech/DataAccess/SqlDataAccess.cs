using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PhotoSpeech.Options;
using System.Data;

namespace PhotoSpeech.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T>(string sql);
        Task SaveData(string sql);
    }
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly DatabaseOptions _databaseOptions;

        public SqlDataAccess(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions.Value;
        }

        public async Task<List<T>> LoadData<T>(string sql)
        {
            var connectionString = _databaseOptions.PhotoSpeachDb;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql);
                return data.ToList();
            }
        }

        public async Task SaveData(string sql)
        {
            var connectionString = _databaseOptions.PhotoSpeachDb;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}

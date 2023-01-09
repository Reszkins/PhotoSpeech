using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PhotoSpeech.Function
{
    public class DailyReset
    {
        [FunctionName("DailyReset")]
        public static async Task Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var connectionString = Environment.GetEnvironmentVariable("DbConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var queryString = "DELETE FROM [dbo].[Scores]";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    var rows = await command.ExecuteNonQueryAsync();
                    log.LogInformation($"{rows} rows were updated");
                }
            }
        }
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FunctionApp1_restapi
{
    class MachineDataQuery
    {
        [FunctionName("GetData")]
        public static async Task<IActionResult> GetData(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "task")] HttpRequest req, ILogger log)
        {
            List<machinedata> data = new List<machinedata>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"Select top (10) MessageID,DeviceID,temperature,humidity,EventProcessedUtcTime,PartitionId,EventEnqueuedUtcTime,Insertedtime from MachineData order by MessageID desc";
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        machinedata task = new machinedata()
                        {
                            MessageID = (int)reader["MessageID"],
                            DeviceId = reader["DeviceID"].ToString(),
                            temperature = (decimal)reader["temperature"],
                            humidity = (decimal)reader["humidity"],
                            EventProcessedUtcTime = (DateTime)reader["EventProcessedUtcTime"],
                            
                            EventEnqueuedUtcTime = (DateTime)reader["EventEnqueuedUtcTime"],
                            Insertedtime = (DateTime)reader["Insertedtime"],
                        };
                        data.Add(task);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (data.Count > 0)
            {
                return new OkObjectResult(data);
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}

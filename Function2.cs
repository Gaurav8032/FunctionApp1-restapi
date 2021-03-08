using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp1_restapi
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static void Run([QueueTrigger("QueueName", Connection = "ConnectionStrings:AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}

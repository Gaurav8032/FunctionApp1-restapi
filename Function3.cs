using System;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
//using RestSharp;
//Following functions runs with cronschedule of 1 min and pushes data into event hub 
namespace FunctionApp1_restapi
{
    public class Function3
    {
        [FunctionName("Function3")]
        [return: EventHub("outputEventHubMessage", Connection = "EventHubConnectionAppSetting")]
        public static string Run([TimerTrigger("*/1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            
            //return $"{DateTime.Now}";

            string json = new WebClient().DownloadString("https://ihistorian-restapi1.azurewebsites.net/api/task");

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            //return $"{DateTime.Now}";
            return $"{json}";
        }



    }

   

    
}

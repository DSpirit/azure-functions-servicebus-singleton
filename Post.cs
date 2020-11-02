using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AzureFunctions.ServiceBus.Singleton
{
    /// <summary>
    /// This class processes a message every second.
    /// </summary>
    public static class Post
    {
        public const int DURATION_PER_CALL = 1000;

        [Singleton]
        [FunctionName("SendMessage")]
        public static async Task Run([ServiceBusTrigger("sbSingleton", Connection = "AzureWebJobsServiceBus")]string myQueueItem, ILogger log)
        {
            var startTime = DateTime.UtcNow;
            log.LogInformation($"C# ServiceBus queue trigger function started message: {DateTime.UtcNow}");
            await Task.Delay(new Random(367).Next(50, 800));
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {DateTime.UtcNow}");
            var endTime = DateTime.UtcNow;

            var duration = endTime.Subtract(startTime);
            if (duration < TimeSpan.FromMilliseconds(DURATION_PER_CALL))
            {
                await Task.Delay(duration);
                log.LogInformation($"C# ServiceBus queue trigger function delayed message: {duration}");
            }

            log.LogInformation($"C# ServiceBus queue trigger function finished message: {DateTime.UtcNow}");
        }
    }
}

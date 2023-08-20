using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace edomizil_functions
{

    public class edomizil_sync_timer_trigger
    {

        private static HttpClient sharedClient = new()
            {
                BaseAddress = new Uri("https://partner.e-domizil.de/webservice/anbieter/ical/3069229_edch.ics?key=KTxCYKA6z6S3pQDBY1g0xg%3D%3D"),
            };

        [FunctionName("edomizil_sync_timer_trigger")]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var response = await sharedClient.GetAsync("");
            response.EnsureSuccessStatusCode();
    
            var jsonResponse = await response.Content.ReadAsStringAsync();
            log.LogInformation($"{jsonResponse}\n");
        }
    }
}

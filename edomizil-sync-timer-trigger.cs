using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Identity;
using Azure.Storage.Blobs;
using Newtonsoft.Json;

namespace edomizil_functions
{

    public class edomizil_sync_timer_trigger
    {



        [FunctionName("edomizil_sync_timer_trigger")]
        public async Task Run([TimerTrigger("0 */10 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var settingsLoader = new SettingsLoader();
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var calendarClient = new HttpClient             {
                BaseAddress = new Uri(settingsLoader.EdomizilCalendarEndpoint)
            };
            var response = await calendarClient.GetAsync("");
            response.EnsureSuccessStatusCode();
    
            var jsonResponse = await response.Content.ReadAsStringAsync();
            log.LogInformation($"Here come the occupancies:\n");
            var occupancies = ICalProcessor.Parse(jsonResponse);

            var occupanciesJson = JsonConvert.SerializeObject(occupancies);

            log.LogInformation($"Num Occupancies: {occupancies.ListOfOccupancies.Count}");
            
            var blobServiceClient = new BlobServiceClient(new Uri(settingsLoader.BlobStorageEndpoint),
            new DefaultAzureCredential());

            var containerCLient = blobServiceClient.GetBlobContainerClient(settingsLoader.ContainerNameForBlobs);      
            var blobName = $"edomizil";
            var blobClient = containerCLient.GetBlobClient(blobName);
            
            var blobContent = BinaryData.FromString(occupanciesJson);
            var uploadResult = blobClient.Upload(blobContent, true);

        }

    }
}

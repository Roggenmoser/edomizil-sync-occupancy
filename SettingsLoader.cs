using System;

namespace edomizil_functions
{
    public class SettingsLoader {

        public string BlobStorageEndpoint { get; }
        public string ContainerNameForBlobs { get; }
        public string EdomizilCalendarEndpoint { get; }

        public SettingsLoader(){
            BlobStorageEndpoint = getEnvVariable("BlobStorageEndpoint");
            ContainerNameForBlobs = getEnvVariable("ContainerNameForBlobs");
            EdomizilCalendarEndpoint = getEnvVariable("EdomizilCalendarEndpoint");
        }

        private string getEnvVariable(string key){
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }
    }
}
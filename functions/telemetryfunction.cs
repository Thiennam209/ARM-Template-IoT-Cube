using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Collections.Generic;

namespace My.Function
{
    // This class processes telemetry events from IoT Hub, reads temperature of a device
    // and sets the "Temperature" property of the device with the value of the telemetry.
    public class telemetryfunction
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static string adtServiceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");

        [FunctionName("telemetryfunction")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            try
            {
                // After this is deployed, you need to turn the Managed Identity Status to "On",
                // Grab Object Id of the function and assigned "Azure Digital Twins Owner (Preview)" role
                // to this function identity in order for this function to be authorized on ADT APIs.
                //Authenticate with Digital Twins
                var credentials = new DefaultAzureCredential();
                log.LogInformation(credentials.ToString());
                DigitalTwinsClient client = new DigitalTwinsClient(
                    new Uri(adtServiceUrl), credentials, new DigitalTwinsClientOptions
                    { Transport = new HttpClientTransport(httpClient) });
                log.LogInformation($"ADT service client connection created.");

                if (eventGridEvent != null && eventGridEvent.Data != null)
                {

                    JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
                    // string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];
                    // var ID = deviceMessage["body"]["deviceid"];
                    string deviceId = "deviceid1";
                    var ID = "deviceid1";
                    var TimeInterval = deviceMessage["body"]["TimeInterval"];
                    var gyroscopeX = deviceMessage["body"]["gyroscopeX"];
                    var gyroscopeY = deviceMessage["body"]["gyroscopeY"];
                    var gyroscopeZ = deviceMessage["body"]["gyroscopeZ"];

                    log.LogInformation($"Device:{deviceId} Device Id is: {ID}");
                    log.LogInformation($"Device:{deviceId} Time interval is: {TimeInterval}");
                    log.LogInformation($"Device:{deviceId} gyroscopeX is: {gyroscopeX}");

                    var updateProperty = new JsonPatchDocument();
                    var turbineTelemetry = new Dictionary<string, Object>()
                    {
                        ["deviceid"] = ID,
                        ["TimeInterval"] = TimeInterval,
                        ["gyroscopeX"] = gyroscopeX,
                        ["gyroscopeY"] = gyroscopeY,
                        ["gyroscopeZ"] = gyroscopeZ,
                    };
                    updateProperty.AppendAdd("/deviceid", "deviceid1");

                    log.LogInformation(updateProperty.ToString());
                    try
                    {
                        await client.PublishTelemetryAsync(deviceId, Guid.NewGuid().ToString(), JsonConvert.SerializeObject(turbineTelemetry));
                    }
                    catch (Exception e)
                    {
                        log.LogInformation(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }
        }
    }
}
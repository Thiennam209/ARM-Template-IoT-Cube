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
                    string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];
                    var ID = deviceMessage["body"]["storeid"];
                    var TimeInterval = deviceMessage["body"]["TimeInterval"];
                    var ShelfId = deviceMessage["body"]["ShelfId"];
                    var ProductId = deviceMessage["body"]["ProductId"];
                    var ProductName = deviceMessage["body"]["ProductName"];
                    var ProductCategory = deviceMessage["body"]["ProductCategory"];
                    var ProductOnShelfName = deviceMessage["body"]["ProductOnShelfName"];
                    var ProductSellingRank = deviceMessage["body"]["ProductSellingRank"];
                    var ProductSalesLastDay = deviceMessage["body"]["ProductSalesLastDay"];
                    var ProductSalesLastMonth = deviceMessage["body"]["ProductSalesLastMonth"];
                    var ProductSalesLast3Months = deviceMessage["body"]["ProductSalesLast3Months"];
                    var ProductPrice = deviceMessage["body"]["ProductPrice"];
                    var ProductCost = deviceMessage["body"]["ProductCost"];
                    var ProductSalesOffPercents = deviceMessage["body"]["ProductSalesOffPercents"];
                    var ProductMarketingCost = deviceMessage["body"]["ProductMarketingCost"];
                    var ProductProfitPerItem = deviceMessage["body"]["ProductProfitPerItem"];
                    var ProductRevenueLastDay = deviceMessage["body"]["ProductRevenueLastDay"];
                    var ProductRevenueLastMonth = deviceMessage["body"]["ProductRevenueLastMonth"];
                    var ProductRevenueLast3Months = deviceMessage["body"]["ProductRevenueLast3Months"];
                    var ProductProfitLastDay = deviceMessage["body"]["ProductProfitLastDay"];
                    var ProductProfitLastMonth = deviceMessage["body"]["ProductProfitLastMonth"];
                    var ProductProfitLast3Months = deviceMessage["body"]["ProductProfitLast3Months"];
                    var ProductImageURL = deviceMessage["body"]["ProductImageURL"];
                    var ShelfRank = deviceMessage["body"]["ShelfRank"];
                    var ShelfCustomerQuantityLastDay = deviceMessage["body"]["ShelfCustomerQuantityLastDay"];
                    var ShelfCustomerQuantityLastMonth = deviceMessage["body"]["ShelfCustomerQuantityLastMonth"];
                    var ShelfCustomerQuantityLast3Months = deviceMessage["body"]["ShelfCustomerQuantityLast3Months"];
                    var ShelfProductNames = deviceMessage["body"]["ShelfProductNames"];
                    var ShelfItemQuantity = deviceMessage["body"]["ShelfItemQuantity"];
                    var ShelfSalesLastDay = deviceMessage["body"]["ShelfSalesLastDay"];
                    var ShelfSalesLastMonth = deviceMessage["body"]["ShelfSalesLastMonth"];
                    var ShelfSalesLast3Months = deviceMessage["body"]["ShelfSalesLast3Months"];
                    var ConversionRateLastDay = deviceMessage["body"]["ConversionRateLastDay"];
                    var ConversionRateLastMonth = deviceMessage["body"]["ConversionRateLastMonth"];
                    var ConversionRateLast3Months = deviceMessage["body"]["ConversionRateLast3Months"];
                    var ShelfRevenueLastDay = deviceMessage["body"]["ShelfRevenueLastDay"];
                    var ShelfRevenueLastMonth = deviceMessage["body"]["ShelfRevenueLastMonth"];
                    var ShelfRevenueLast3Months = deviceMessage["body"]["ShelfRevenueLast3Months"];
                    var ShelfProfitLastDay = deviceMessage["body"]["ShelfProfitLastDay"];
                    var ShelfProfitLastMonth = deviceMessage["body"]["ShelfProfitLastMonth"];
                    var ShelfProfitLast3Months = deviceMessage["body"]["ShelfProfitLast3Months"];
                    var StoreSalesLastDay = deviceMessage["body"]["StoreSalesLastDay"];
                    var StoreSalesLastMonth = deviceMessage["body"]["StoreSalesLastMonth"];
                    var StoreSalesLast3Months = deviceMessage["body"]["StoreSalesLast3Months"];
                    var StoreRevenueLastDay = deviceMessage["body"]["StoreRevenueLastDay"];
                    var StoreRevenueLastMonth = deviceMessage["body"]["StoreRevenueLastMonth"];
                    var StoreRevenueLast3Months = deviceMessage["body"]["StoreRevenueLast3Months"];
                    var StoreProfitLastDay = deviceMessage["body"]["StoreProfitLastDay"];
                    var StoreProfitLastMonth = deviceMessage["body"]["StoreProfitLastMonth"];
                    var StoreProfitLast3Months = deviceMessage["body"]["StoreProfitLast3Months"];

                    log.LogInformation($"Device:{deviceId} Device Id is: {ID}");
                    log.LogInformation($"Device:{deviceId} Time interval is: {TimeInterval}");
                    log.LogInformation($"Device:{deviceId} ShelfId is: {ShelfId}");
                    log.LogInformation($"Device:{deviceId} ProductId is: {ProductId}");
                    log.LogInformation($"Device: {deviceId} ProductName is: {ProductName}");
                    log.LogInformation($"Device: {deviceId} ProductCategory is: {ProductCategory}");
                    // log.LogInformation($"Device: {deviceId} ProductOnShelfName is: {ProductOnShelfName}");
                    // log.LogInformation($"Device: {deviceId} ProductSellingRank is: {ProductSellingRank}");
                    // log.LogInformation($"Device: {deviceId} ProductSalesLastDay: {ProductSalesLastDay}");
                    // log.LogInformation($"Device: {deviceId} ProductSalesLastMonth: {ProductSalesLastMonth}");
                    // log.LogInformation($"Device: {deviceId} ProductSalesLast3Months: {ProductSalesLast3Months}");
                    // log.LogInformation($"Device: {deviceId} ProductPrice is: {ProductPrice}");
                    // log.LogInformation($"Device: {deviceId} ProductCost is: {ProductCost}");
                    // log.LogInformation($"Device: {deviceId} ProductSalesOffPercents is: {ProductSalesOffPercents}");
                    // log.LogInformation($"Device: {deviceId} ProductMarketingCost is: {ProductMarketingCost}");
                    // log.LogInformation($"Device: {deviceId} ProductProfitPerItem is: {ProductProfitPerItem}");
                    // log.LogInformation($"Device: {deviceId} ProductRevenueLastDay is: {ProductRevenueLastDay}");
                    // log.LogInformation($"Device: {deviceId} ProductRevenueLastMonth is: {ProductRevenueLastMonth}");
                    // log.LogInformation($"Device: {deviceId} ProductRevenueLast3Months is: {ProductRevenueLast3Months}");
                    // log.LogInformation($"Device: {deviceId} ProductProfitLastDay is: {ProductProfitLastDay}");
                    // log.LogInformation($"Device: {deviceId} ProductProfitLastMonth is: {ProductProfitLastMonth}");
                    // log.LogInformation($"Device: {deviceId} ProductProfitLast3Months is: {ProductProfitLast3Months}");
                    // log.LogInformation($"Device: {deviceId} ProductImageURL is: {ProductImageURL}");
                    log.LogInformation($"Device: {deviceId} ShelfRank is: {ShelfRank}");
                    // log.LogInformation($"Device: {deviceId} ShelfCustomerQuantityLastDay is: {ShelfCustomerQuantityLastDay}");
                    // log.LogInformation($"Device: {deviceId} ShelfCustomerQuantityLastMonth is: {ShelfCustomerQuantityLastMonth}");
                    // log.LogInformation($"Device: {deviceId} ShelfCustomerQuantityLast3Months is: {ShelfCustomerQuantityLast3Months}");
                    // log.LogInformation($"Device: {deviceId} ShelfProductNames is: {ShelfProductNames}");
                    // log.LogInformation($"Device: {deviceId} ShelfItemQuantity is: {ShelfItemQuantity}");
                    // log.LogInformation($"Device: {deviceId} ShelfSalesLastDay is: {ShelfSalesLastDay}");
                    // log.LogInformation($"Device: {deviceId} ShelfSalesLastMonth is: {ShelfSalesLastMonth}");
                    // log.LogInformation($"Device: {deviceId} ShelfSalesLast3Months is: {ShelfSalesLast3Months}");
                    // log.LogInformation($"Device: {deviceId} ConversionRateLastDay is: {ConversionRateLastDay}");
                    // log.LogInformation($"Device: {deviceId} ConversionRateLastMonth is: {ConversionRateLastMonth}");
                    // log.LogInformation($"Device: {deviceId} ConversionRateLast3Months is: {ConversionRateLast3Months}");
                    // log.LogInformation($"Device: {deviceId} ShelfRevenueLastDay is: {ShelfRevenueLastDay}");
                    // log.LogInformation($"Device: {deviceId} ShelfRevenueLastMonth is: {ShelfRevenueLastMonth}");
                    // log.LogInformation($"Device: {deviceId} ShelfRevenueLast3Months is: {ShelfRevenueLast3Months}");
                    // log.LogInformation($"Device: {deviceId} ShelfProfitLastDay is: {ShelfProfitLastDay}");
                    // log.LogInformation($"Device: {deviceId} ShelfProfitLastMonth is: {ShelfProfitLastMonth}");
                    // log.LogInformation($"Device: {deviceId} ShelfProfitLast3Months is: {ShelfProfitLast3Months}");
                    log.LogInformation($"Device: {deviceId} StoreSalesLastDay is: {StoreSalesLastDay}");
                    log.LogInformation($"Device: {deviceId} StoreSalesLastMonth is: {StoreSalesLastMonth}");
                    log.LogInformation($"Device: {deviceId} StoreSalesLast3Months is: {StoreSalesLast3Months}");
                    log.LogInformation($"Device: {deviceId} StoreRevenueLastDay is: {StoreRevenueLastDay}");
                    log.LogInformation($"Device: {deviceId} StoreRevenueLastMonth is: {StoreRevenueLastMonth}");
                    log.LogInformation($"Device: {deviceId} StoreRevenueLast3Months is: {StoreRevenueLast3Months}");
                    log.LogInformation($"Device: {deviceId} StoreProfitLastDay is: {StoreProfitLastDay}");
                    log.LogInformation($"Device: {deviceId} StoreProfitLastMonth is: {StoreProfitLastMonth}");
                    log.LogInformation($"Device: {deviceId} StoreProfitLast3Months is: {StoreProfitLast3Months}");

                    var updateProperty = new JsonPatchDocument();
                    var turbineTelemetry = new Dictionary<string, Object>()
                    {
                        ["storeid"] = ID,
                        ["TimeInterval"] = TimeInterval,
                        ["ShelfId"] = ShelfId,
                        ["ProductId"] = ProductId,
                        ["ProductName"] = ProductName,
                        ["ProductCategory"] = ProductCategory,
                        ["ProductOnShelfName"] = ProductOnShelfName,
                        ["ProductSellingRank"] = ProductSellingRank,
                        ["ProductSalesLastDay"] = ProductSalesLastDay,
                        ["ProductSalesLastMonth"] = ProductSalesLastMonth,
                        ["ProductSalesLast3Months"] = ProductSalesLast3Months,
                        ["ProductPrice"] = ProductPrice,
                        ["ProductCost"] = ProductCost,
                        ["ProductSalesOffPercents"] = ProductSalesOffPercents,
                        ["ProductMarketingCost"] = ProductMarketingCost,
                        ["ProductProfitPerItem"] = ProductProfitPerItem,
                        ["ProductRevenueLastDay"] = ProductRevenueLastDay,
                        ["ProductRevenueLastMonth"] = ProductRevenueLastMonth,
                        ["ProductRevenueLast3Months"] = ProductRevenueLast3Months,
                        ["ProductProfitLastDay"] = ProductProfitLastDay,
                        ["ProductProfitLastMonth"] = ProductProfitLastMonth,
                        ["ProductProfitLast3Months"] = ShelfRank,
                        ["ProductImageURL"] = ProductImageURL,
                        ["ShelfRank"] = ShelfRank,
                        ["ShelfCustomerQuantityLastDay"] = ShelfCustomerQuantityLastDay,
                        ["ShelfCustomerQuantityLastMonth"] = ShelfCustomerQuantityLastMonth,
                        ["ShelfCustomerQuantityLast3Months"] = ShelfCustomerQuantityLast3Months,
                        ["ShelfProductNames"] = ShelfProductNames,
                        ["ShelfItemQuantity"] = ShelfItemQuantity,
                        ["ShelfSalesLastDay"] = ShelfSalesLastDay,
                        ["ShelfSalesLastMonth"] = ShelfSalesLastMonth,
                        ["ShelfSalesLast3Months"] = ShelfSalesLast3Months,
                        ["ConversionRateLastDay"] = ConversionRateLastDay,
                        ["ConversionRateLastMonth"] = ConversionRateLastMonth,
                        ["ConversionRateLast3Months"] = ConversionRateLast3Months,
                        ["ShelfRevenueLastDay"] = ShelfRevenueLastDay,
                        ["ShelfRevenueLastMonth"] = ShelfRevenueLastMonth,
                        ["ShelfRevenueLast3Months"] = ShelfRevenueLast3Months,
                        ["ShelfProfitLastDay"] = ShelfProfitLastDay,
                        ["ShelfProfitLastMonth"] = ShelfProfitLastMonth,
                        ["ShelfProfitLast3Months"] = ShelfProfitLast3Months,
                        ["StoreSalesLastDay"] = StoreSalesLastDay,
                        ["StoreSalesLastMonth"] = StoreSalesLastMonth,
                        ["StoreSalesLast3Months"] = StoreSalesLast3Months,
                        ["StoreRevenueLastDay"] = StoreRevenueLastDay,
                        ["StoreRevenueLastMonth"] = StoreRevenueLastMonth,
                        ["StoreRevenueLast3Months"] = StoreRevenueLast3Months,
                        ["StoreProfitLastDay"] = StoreProfitLastDay,
                        ["StoreProfitLastMonth"] = StoreProfitLastMonth,
                        ["StoreProfitLast3Months"] = StoreProfitLast3Months,
                    };
                    updateProperty.AppendAdd("/storeid", ID.Value<string>());

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
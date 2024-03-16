using Azure.Communication.CallAutomation;
using System.Reflection;
using System.Text.Json;

namespace AcsEmulatorAPI.Services;

public class WebhookPublishingService(ILogger<WebhookPublishingService> logger, HttpClient httpClient)
{
    public async Task SendCallConnectedEventAsync(Uri endpoint, string callConnectionId)
    {
        // TODO: add proper payload
        var callConnected = CallAutomationModelFactory.CallConnected(callConnectionId, "serverCallId", "correlationId", "operationContext");

        /*Type callConnectedType = callConnected.GetType();

        Console.WriteLine("Properties of CallConnected class:");
        foreach (PropertyInfo property in callConnectedType.GetProperties())
        {
            Console.WriteLine($"{property.Name}: {property.PropertyType}");
        }*/
        var callConnectionPayload = new
        {
            version = "2023-10-15",
            callConnectionId = callConnectionId,
            serverCallId = "serverCallId",
            correlationId = Guid.NewGuid().ToString(),
            publicEventType = "Microsoft.Communication.CallConnected"
        };

        // Serialize callConnectionPayload into a JSON string
        string callConnectionPayloadJson = JsonSerializer.Serialize(callConnectionPayload);

        // Convert the JSON string to BinaryData
        BinaryData binaryData = new BinaryData(callConnectionPayloadJson);

        var payloads = new List<object>();

        var payload = new
        {
            data = binaryData,
            id = Guid.NewGuid().ToString(),
            source = "calling/callConnections/" + Guid.NewGuid().ToString(),
            type = "Microsoft.Communication.CallConnected",
            time = DateTime.Now,
            dataschema = JsonSerializer.Serialize(callConnectionPayload), // Convert to string
            datacontenttype = JsonSerializer.Serialize(callConnectionPayload), // Convert to string
            subject = JsonSerializer.Serialize(callConnectionPayload), // Convert to string
            specversion = "1.0"
        };

        payloads.Add(payload);

        await httpClient.PostAsJsonAsync(endpoint, payloads);

    }
}
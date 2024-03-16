using Azure.Communication.CallAutomation;
using System.Reflection;

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
        var callConnectionPayload =
        new
        {
            version = "2023-10-15",
            callConnectionId = callConnectionId,
            serverCallId = "serverCallId",
            correlationId = Guid.NewGuid().ToString(),
            publicEventType = "Microsoft.Communication.CallConnected"
        };

        var payloads = new List<object>();

        var payload = new
        {
            data = callConnectionPayload,
            id = Guid.NewGuid().ToString(),
            source = "calling/callConnections/" + Guid.NewGuid().ToString(),
            type = "Microsoft.Communication.CallConnected",
            time = DateTime.Now,
            dataSchema = callConnectionPayload,
            dataContentType = callConnectionPayload,
            subject = callConnectionPayload
        };

        payloads.Add(payload);

        await httpClient.PostAsJsonAsync(endpoint, payloads);
    }
}
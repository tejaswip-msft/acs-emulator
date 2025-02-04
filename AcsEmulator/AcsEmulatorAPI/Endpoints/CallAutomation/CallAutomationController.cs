﻿using AcsEmulatorAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace AcsEmulatorAPI.Endpoints.CallAutomation
{
    // https://github.com/Azure/azure-rest-api-specs/blob/main/specification/communication/data-plane/CallAutomation/stable/2023-10-15/communicationservicescallautomation.json
    public class CallAutomationController: IDisposable
    {
        // need to figure out DI to make this less hacky, only setting with CreateCall route now
        private AcsDbContext _dbContext;
        private readonly CallAutomationWebSockets _sockets;
        private readonly ILogger<Program> _logger;
        private Dictionary<string, Guid> _connections = new();

        public CallAutomationController(CallAutomationWebSockets sockets, ILogger<Program> logger)
        {
            _sockets = sockets;
            _logger = logger;
        }

        public void ListenToIncomingMessages() => _sockets.OnIncomingMessage += HandleIncomingMessage;

        public void Dispose() => _sockets.OnIncomingMessage -= HandleIncomingMessage;

        public void HandleIncomingMessage(object _sender, IncomingMessageEventArgs ev)
        {
            _ = HandleAsync();

            async Task HandleAsync()
            {
                switch (ev.Action)
                {
                    case "acceptCall":
                        {
                            if (_dbContext != null && _connections.TryGetValue(ev.From, out var connectionId))
                            {
                                var connection = await _dbContext.CallConnections.FindAsync(connectionId);
                                if (connection != null)
                                {
                                    connection.CallConnectionState = CallConnectionState.Connected;
                                    await _dbContext.SaveChangesAsync();
                                }
                            }
                            break;
                        }
                }
            }
        }

        public void AddEndpoints(WebApplication app)
        {
            string emulatorDeviceNumber = app.Configuration.GetValue<string>("EmulatorDevicePhoneNumber")!;

            app.MapPost("/calling/callConnections", async (AcsDbContext db, CallAutomationWebSockets sockets, CreateCallRequest req) =>
            {
                // MVP0: PhoneNumber places a call to a CommunicationUser

                _dbContext = db;
                var callConnection = CallConnection.CreateNew(req.CallbackUri, req.SourceCallerIdNumber?.Value);

                callConnection.AddTargets(req.Targets);

                await db.CallConnections.AddAsync(callConnection);
                await db.SaveChangesAsync();

                var result = new
                {
                    CallConnectionProperties = new
                    {
                        CallConnectionId = callConnection.Id,
                        callConnection.AnsweredBy,
                        callConnection.CallConnectionState,
                        callConnection.CallbackUri,
                        callConnection.CorrelationId,
                        callConnection.ServerCallId,
                        callConnection.Source,
                        callConnection.SourceCallerIdNumber,
                        callConnection.SourceDisplayName,
                        Targets = callConnection.Targets.Select(x => new CommunicationIdentifier(x.RawId)).ToList()
                    }
                };

                if (ContainsEmulatorDeviceNumber(req.Targets))
                {
                    // place call to Emulator UI client
                    await sockets.MakePhoneCall(emulatorDeviceNumber, req.SourceCallerIdNumber?.Value, req.SourceDisplayName);
                    _connections.Add(emulatorDeviceNumber, callConnection.Id);
                }

                return Results.Created($"/calling/callConnections/{callConnection.Id}", result);
            });

            bool ContainsEmulatorDeviceNumber(IEnumerable<CommunicationIdentifier> targets)
                => targets.Any(x => x.PhoneNumber?.Value == emulatorDeviceNumber);

            app.MapPost("/calling/callConnections/{callConnectionId}:play", async (AcsDbContext db, CallAutomationWebSockets sockets, string callConnectionId, PlayRequest req) =>
            {
                var connection = await db.CallConnections.FindAsync(Guid.Parse(callConnectionId));
                if (connection is null) {
                    // todo: validate what ACS is really returning in this case
                    return Results.NotFound();
                };

                // todo: check that connection state is "connected" - skipping for now because we haven't wired up the accept call from emulator phone client flow

                List<TextSource> textSources = req.playSources?.Where(x => x.kind == PlaySourceType.Text && x.text is not null).Select(x => x.text).Cast<TextSource>().ToList();
                if (req.playTo.PhoneNumber?.Value == emulatorDeviceNumber && !textSources.IsNullOrEmpty())
                {
                    // tell Emulator UI client to synthesize text - real ACS will send audio, for our emulator the Browser's built-in speech APIs have to do
                    await sockets.PlayText(emulatorDeviceNumber, connection.SourceCallerIdNumber, textSources!.First().text);
                }

                return Results.Accepted();
            });

            app.MapGet("/calling/callConnections/{callConnectionId}", async (string callConnectionId, AcsDbContext db) =>
            {
                // Retrieve the CallConnection entity from the database based on the callConnectionId
                var callConnection = await db.CallConnections.FindAsync(Guid.Parse(callConnectionId));

                if (callConnection == null)
                {
                    // If the CallConnection entity is not found, return a 404 Not Found response
                    return Results.NotFound("Call connection not found.");
                }

                // Return the CallConnection entity properties as the response
                return Results.Ok(new
                {
                    callConnection.Id,
                    callConnection.AnsweredBy,
                    callConnection.CallConnectionState,
                    callConnection.CallbackUri,
                    callConnection.CorrelationId,
                    callConnection.ServerCallId,
                    callConnection.Source,
                    callConnection.SourceCallerIdNumber,
                    callConnection.SourceDisplayName,
                    Targets = callConnection.Targets.Select(x => new CommunicationIdentifier(x.RawId)).ToArray()
                });
            });




        }
    }
}

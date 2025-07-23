using System.Text.Json;
using Evolution.Client.CSharp.Models.Instance;

namespace Evolution.Client.CSharp.Tests.Services;

public class InstanceResponseDeserializationTests
{
    [Fact]
    public void Should_Deserialize_Instance_Array_Correctly()
    {
        // Arrange
        var jsonResponse = """
        [
            {
                "id": "347f2197-7026-489c-b3db-ac40a08c0e91",
                "name": "Elias",
                "connectionStatus": "close",
                "ownerJid": null,
                "profileName": null,
                "profilePicUrl": null,
                "integration": "WHATSAPP-BAILEYS",
                "number": null,
                "businessId": null,
                "token": "9E3F517F-8CFC-43DD-8CBD-0BDD6B85F4D1",
                "clientName": "evolution_exchange",
                "disconnectionReasonCode": null,
                "disconnectionObject": null,
                "disconnectionAt": null,
                "createdAt": "2025-07-23T13:48:10.241Z",
                "updatedAt": "2025-07-23T13:48:10.241Z",
                "Chatwoot": null,
                "Proxy": null,
                "Rabbitmq": null,
                "Sqs": null,
                "Websocket": null,
                "Setting": {
                    "id": "cmdg0qt8s0009n54q3gs93dja",
                    "rejectCall": false,
                    "msgCall": "",
                    "groupsIgnore": false,
                    "alwaysOnline": false,
                    "readMessages": false,
                    "readStatus": false,
                    "syncFullHistory": false,
                    "wavoipToken": "",
                    "createdAt": "2025-07-23T13:48:10.251Z",
                    "updatedAt": "2025-07-23T13:48:10.251Z",
                    "instanceId": "347f2197-7026-489c-b3db-ac40a08c0e91"
                },
                "_count": {
                    "Message": 0,
                    "Contact": 0,
                    "Chat": 0
                }
            }
        ]
        """;

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Act
        var instances = JsonSerializer.Deserialize<List<InstanceResponse>>(jsonResponse, jsonOptions);
        var instancesResponse = new InstancesResponse(instances!);

        // Assert
        Assert.NotNull(instances);
        Assert.Single(instances);
        Assert.NotNull(instancesResponse);
        Assert.Single(instancesResponse);

        var instance = instances.First();
        Assert.Equal("347f2197-7026-489c-b3db-ac40a08c0e91", instance.Id);
        Assert.Equal("Elias", instance.Name);
        Assert.Equal("close", instance.ConnectionStatus);
        Assert.Equal("WHATSAPP-BAILEYS", instance.Integration);
        Assert.Equal("9E3F517F-8CFC-43DD-8CBD-0BDD6B85F4D1", instance.Token);
        Assert.Equal("evolution_exchange", instance.ClientName);

        // Verifica as configurações
        Assert.NotNull(instance.Setting);
        Assert.Equal("cmdg0qt8s0009n54q3gs93dja", instance.Setting.Id);
        Assert.False(instance.Setting.RejectCall);
        Assert.False(instance.Setting.GroupsIgnore);
        Assert.False(instance.Setting.AlwaysOnline);
        Assert.False(instance.Setting.ReadMessages);
        Assert.False(instance.Setting.ReadStatus);
        Assert.False(instance.Setting.SyncFullHistory);
        Assert.Equal("347f2197-7026-489c-b3db-ac40a08c0e91", instance.Setting.InstanceId);

        // Verifica as contagens
        Assert.NotNull(instance.Count);
        Assert.Equal(0, instance.Count.Message);
        Assert.Equal(0, instance.Count.Contact);
        Assert.Equal(0, instance.Count.Chat);
    }
}
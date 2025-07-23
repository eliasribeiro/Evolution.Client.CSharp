using System.Text.Json;
using Evolution.Client.CSharp.Models.Instance;
using Xunit;

namespace Evolution.Client.CSharp.Tests.Services;

public class DisconnectionObjectTests
{
    [Fact]
    public void Should_Deserialize_Instance_With_DisconnectionObject_Correctly()
    {
        // Arrange - JSON real com disconnectionObject preenchido
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
            "disconnectionReasonCode": 401,
            "disconnectionObject": "{\"error\":{\"data\":null,\"isBoom\":true,\"isServer\":false,\"output\":{\"statusCode\":401,\"payload\":{\"statusCode\":401,\"error\":\"Unauthorized\",\"message\":\"Log out instance: Elias\"},\"headers\":{}}},\"date\":\"2025-07-23T14:06:14.637Z\"}",
            "disconnectionAt": "2025-07-23T14:06:14.642Z",
            "createdAt": "2025-07-23T13:48:10.241Z",
            "updatedAt": "2025-07-23T14:06:14.652Z",
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

        // Act
        var result = JsonSerializer.Deserialize<List<InstanceResponse>>(jsonResponse);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);

        var instance = result[0];
        
        // Verifica propriedades básicas
        Assert.Equal("347f2197-7026-489c-b3db-ac40a08c0e91", instance.Id);
        Assert.Equal("Elias", instance.Name);
        Assert.Equal("close", instance.ConnectionStatus);
        Assert.Null(instance.OwnerJid);
        Assert.Null(instance.ProfileName);
        Assert.Null(instance.ProfilePicUrl);
        Assert.Equal("WHATSAPP-BAILEYS", instance.Integration);
        Assert.Null(instance.Number);
        Assert.Null(instance.BusinessId);
        Assert.Equal("9E3F517F-8CFC-43DD-8CBD-0BDD6B85F4D1", instance.Token);
        Assert.Equal("evolution_exchange", instance.ClientName);
        
        // Verifica propriedades de desconexão
        Assert.Equal(401, instance.DisconnectionReasonCode);
        Assert.NotNull(instance.DisconnectionObject);
        Assert.NotNull(instance.DisconnectionAt);
        
        // Verifica se o disconnectionObject é uma string JSON válida
        var disconnectionObjectStr = instance.DisconnectionObject?.ToString();
        Assert.NotNull(disconnectionObjectStr);
        Assert.Contains("error", disconnectionObjectStr);
        Assert.Contains("Unauthorized", disconnectionObjectStr);
        Assert.Contains("Log out instance: Elias", disconnectionObjectStr);
        
        // Verifica datas
        Assert.Equal(DateTime.Parse("2025-07-23T13:48:10.241Z").ToUniversalTime(), instance.CreatedAt.ToUniversalTime());
        Assert.Equal(DateTime.Parse("2025-07-23T14:06:14.652Z").ToUniversalTime(), instance.UpdatedAt.ToUniversalTime());
        Assert.Equal(DateTime.Parse("2025-07-23T14:06:14.642Z").ToUniversalTime(), instance.DisconnectionAt?.ToUniversalTime());
        
        // Verifica objetos nulos
        Assert.Null(instance.Chatwoot);
        Assert.Null(instance.Proxy);
        Assert.Null(instance.Rabbitmq);
        Assert.Null(instance.Sqs);
        Assert.Null(instance.Websocket);
        
        // Verifica configurações
        Assert.NotNull(instance.Setting);
        Assert.Equal("cmdg0qt8s0009n54q3gs93dja", instance.Setting!.Id);
        Assert.False(instance.Setting!.RejectCall);
        Assert.Equal("", instance.Setting!.MsgCall);
        Assert.False(instance.Setting!.GroupsIgnore);
        Assert.False(instance.Setting!.AlwaysOnline);
        Assert.False(instance.Setting!.ReadMessages);
        Assert.False(instance.Setting!.ReadStatus);
        Assert.False(instance.Setting!.SyncFullHistory);
        Assert.Equal("", instance.Setting!.WavoipToken);
        Assert.Equal("347f2197-7026-489c-b3db-ac40a08c0e91", instance.Setting!.InstanceId);
        
        // Verifica contagens
        Assert.NotNull(instance.Count);
        Assert.Equal(0, instance.Count!.Message);
        Assert.Equal(0, instance.Count!.Contact);
        Assert.Equal(0, instance.Count!.Chat);
    }
    
    [Fact]
    public void Should_Handle_DisconnectionReasonCode_As_Integer()
    {
        // Arrange - Testa se o disconnectionReasonCode pode ser deserializado como int
        var jsonResponse = """
        [
          {
            "id": "test-id",
            "name": "Test",
            "connectionStatus": "close",
            "integration": "WHATSAPP-BAILEYS",
            "token": "test-token",
            "clientName": "test-client",
            "disconnectionReasonCode": 401,
            "disconnectionObject": "{}",
            "disconnectionAt": "2025-07-23T14:06:14.642Z",
            "createdAt": "2025-07-23T13:48:10.241Z",
            "updatedAt": "2025-07-23T14:06:14.652Z",
            "Setting": {
              "id": "test-setting-id",
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
              "instanceId": "test-id"
            },
            "_count": {
              "Message": 0,
              "Contact": 0,
              "Chat": 0
            }
          }
        ]
        """;

        // Act
        var result = JsonSerializer.Deserialize<List<InstanceResponse>>(jsonResponse);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(401, result[0].DisconnectionReasonCode);
    }
}
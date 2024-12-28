using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance.Create
{
    public class ResponseInstance
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("connectionStatus")]
        public string ConnectionStatus { get; set; }

        [JsonPropertyName("ownerJid")]
        public string? OwnerJid { get; set; }

        [JsonPropertyName("profileName")]
        public string? ProfileName { get; set; }

        [JsonPropertyName("profilePicUrl")]
        public string? ProfilePicUrl { get; set; }

        [JsonPropertyName("integration")]
        public string Integration { get; set; }

        [JsonPropertyName("number")]
        public string? Number { get; set; }

        [JsonPropertyName("businessId")]
        public string? BusinessId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("clientName")]
        public string ClientName { get; set; }

        [JsonPropertyName("disconnectionReasonCode")]
        public string? DisconnectionReasonCode { get; set; }

        [JsonPropertyName("disconnectionObject")]
        public object? DisconnectionObject { get; set; }

        [JsonPropertyName("disconnectionAt")]
        public DateTime? DisconnectionAt { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("Chatwoot")]
        public object? Chatwoot { get; set; }

        [JsonPropertyName("Proxy")]
        public object? Proxy { get; set; }

        [JsonPropertyName("Rabbitmq")]
        public object? Rabbitmq { get; set; }

        [JsonPropertyName("Sqs")]
        public object? Sqs { get; set; }

        [JsonPropertyName("Websocket")]
        public object? Websocket { get; set; }

        [JsonPropertyName("Setting")]
        public Setting Setting { get; set; }

        [JsonPropertyName("_count")]
        public Count Count { get; set; }
    }

    public class Setting
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("rejectCall")]
        public bool RejectCall { get; set; }

        [JsonPropertyName("msgCall")]
        public string MsgCall { get; set; }

        [JsonPropertyName("groupsIgnore")]
        public bool GroupsIgnore { get; set; }

        [JsonPropertyName("alwaysOnline")]
        public bool AlwaysOnline { get; set; }

        [JsonPropertyName("readMessages")]
        public bool ReadMessages { get; set; }

        [JsonPropertyName("readStatus")]
        public bool ReadStatus { get; set; }

        [JsonPropertyName("syncFullHistory")]
        public bool SyncFullHistory { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("instanceId")]
        public string InstanceId { get; set; }
    }

    public class Count
    {
        [JsonPropertyName("Message")]
        public int Message { get; set; }

        [JsonPropertyName("Contact")]
        public int Contact { get; set; }

        [JsonPropertyName("Chat")]
        public int Chat { get; set; }
    }
}

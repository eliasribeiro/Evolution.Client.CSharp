namespace Evolution.Client.CSharp.Models.Instance.Create
{
    public class ResponseInstance
    {
        public string id { get; set; }
        public string name { get; set; }
        public string connectionStatus { get; set; }
        public string? ownerJid { get; set; }
        public string? profileName { get; set; }
        public string? profilePicUrl { get; set; }
        public string integration { get; set; }
        public string? number { get; set; }
        public string? businessId { get; set; }
        public string token { get; set; }
        public string clientName { get; set; }
        public string? disconnectionReasonCode { get; set; }
        public object? disconnectionObject { get; set; }
        public DateTime? disconnectionAt { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public object? Chatwoot { get; set; }
        public object? Proxy { get; set; }
        public object? Rabbitmq { get; set; }
        public object? Sqs { get; set; }
        public object? Websocket { get; set; }
        public Setting Setting { get; set; }
        public Count _count { get; set; }
    }

    public class Setting
    {
        public string id { get; set; }
        public bool rejectCall { get; set; }
        public string msgCall { get; set; }
        public bool groupsIgnore { get; set; }
        public bool alwaysOnline { get; set; }
        public bool readMessages { get; set; }
        public bool readStatus { get; set; }
        public bool syncFullHistory { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string instanceId { get; set; }
    }

    public class Count
    {
        public int Message { get; set; }
        public int Contact { get; set; }
        public int Chat { get; set; }
    }
}

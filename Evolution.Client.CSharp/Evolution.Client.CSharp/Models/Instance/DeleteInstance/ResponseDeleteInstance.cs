namespace Evolution.Client.CSharp.Models.Instance.LogoutInstance
{
    public class ResponseDeleteInstance
    {
        public string Status { get; set; } = string.Empty;
        public bool Error { get; set; }
        public ResponseData Response { get; set; } = new ResponseData();
    }
}

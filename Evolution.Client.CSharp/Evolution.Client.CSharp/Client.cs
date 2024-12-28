using Evolution.Client.CSharp.Services.Messages;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp;

public class EvolutionAuthenticationError : Exception
{
    public EvolutionAuthenticationError(string message) : base(message) { }
}

public class EvolutionNotFoundError : Exception
{
    public EvolutionNotFoundError(string message) : base(message) { }
}

public class EvolutionAPIError : Exception
{
    public EvolutionAPIError(string message) : base(message) { }
}

public class EvolutionClient
{
    private readonly string baseUrl;
    private readonly string apiToken;
    private readonly HttpClient httpClient;

    public InstanceService Instances { get; }
    public MessageService Messages { get; }
    /*public InstanceOperationsService InstanceOperations { get; }
    
    public CallService Calls { get; }
    public ChatService Chat { get; }
    public LabelService Label { get; }
    public ProfileService Profile { get; }
    public GroupService Group { get; }*/

    public EvolutionClient(string baseUrl, string apiToken)
    {
        this.baseUrl = baseUrl.TrimEnd('/');
        this.apiToken = apiToken;
        this.httpClient = new HttpClient();

        Instances = new InstanceService(this);
        Messages = new MessageService(this);
        /*InstanceOperations = new InstanceOperationsService(this);
        
        Calls = new CallService(this);
        Chat = new ChatService(this);
        Label = new LabelService(this);
        Profile = new ProfileService(this);
        Group = new GroupService(this);*/
    }

    private Dictionary<string, string> GetHeaders(string instanceToken = null)
    {
        return new Dictionary<string, string>
    {
        { "apikey", instanceToken ?? apiToken },
        //{ "Content-Type", "application/json" }
    };
    }

    private string GetFullUrl(string endpoint)
    {
        return $"{baseUrl}/{endpoint}";
    }

    private async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new EvolutionAuthenticationError("Falha na autenticação.");
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            throw new EvolutionNotFoundError("Recurso não encontrado.");
        else if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(string))
                return (T)(object)content;
            return JsonSerializer.Deserialize<T>(content);
        }
        else
        {
            string errorDetail = "";
            try
            {
                errorDetail = $" - {await response.Content.ReadAsStringAsync()}";
            }
            catch
            {
                errorDetail = $" - {response.StatusCode}";
            }
            throw new EvolutionAPIError($"Erro na requisição: {response.StatusCode}{errorDetail}");
        }
    }

    public async Task<T> GetAsync<T>(string endpoint, string instanceToken = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, GetFullUrl(endpoint));
        foreach (var header in GetHeaders(instanceToken))
            request.Headers.Add(header.Key, header.Value);

        var response = await httpClient.SendAsync(request);
        return await HandleResponse<T>(response);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data = null, string instanceToken = null, Dictionary<string, (string, byte[], string)> files = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, GetFullUrl(endpoint));

        foreach (var header in GetHeaders(instanceToken))
            request.Headers.Add(header.Key, header.Value);

        if (files != null)
        {
            var multipartContent = new MultipartFormDataContent();

            if (data != null)
            {
                var jsonData = JsonSerializer.Serialize(data);
                var properties = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonData);

                foreach (var prop in properties)
                {
                    multipartContent.Add(new StringContent(prop.Value.ToString()), prop.Key);
                }
            }

            foreach (var file in files)
            {
                var fileContent = new ByteArrayContent(file.Value.Item2);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.Value.Item3);
                multipartContent.Add(fileContent, file.Key, file.Value.Item1);
            }

            request.Content = multipartContent;
        }
        else if (data != null)
        {
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            var json = JsonSerializer.Serialize(data, jsonSerializerOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await httpClient.SendAsync(request);
        return await HandleResponse<T>(response);
    }

    public async Task<T> PutAsync<T>(string endpoint, object data = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, GetFullUrl(endpoint));

        if (data != null)
        {
            var json = JsonSerializer.Serialize(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        foreach (var header in GetHeaders())
            request.Headers.Add(header.Key, header.Value);

        var response = await httpClient.SendAsync(request);
        return await HandleResponse<T>(response);
    }

    public async Task<T> DeleteAsync<T>(string endpoint, string instanceToken = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, GetFullUrl(endpoint));
        foreach (var header in GetHeaders(instanceToken))
            request.Headers.Add(header.Key, header.Value);

        var response = await httpClient.SendAsync(request);
        return await HandleResponse<T>(response);
    }
}

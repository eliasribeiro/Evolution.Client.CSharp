# Evolution.Client.CSharp

# Evolution.API C# Library

Esta biblioteca permite que aplicações C# se comuniquem com a API do Evolution.API, uma plataforma que proporciona o envio de mensagens via WhatsApp.

## Funcionalidades

- [x] Gerenciamento de Instâncias
- [x] Envio de mensagens de texto via WhatsApp
- [ ] Envio de mensagens de mídia (imagens, vídeos, documentos)
- [ ] Suporte a mensagens de grupo
- [ ] Recebimento de notificações de mensagens

## Requisitos

- .NET 8

## Instalação

Para instalar a biblioteca, você pode usar o NuGet Package Manager:

`Evolution.Client.CSharp`

### Exemplo: Envio de mídia

```csharp
var client = new EvolutionClient("https://sua-url-da-api", "SEU_API_KEY");
var mediaRequest = new RequestMediaMessage
{
    Number = "5511999999999",
    Type = "image", // ou "video", "document", etc.
    Caption = "Legenda opcional",
    FileName = "foto.jpg",
    FileBytes = File.ReadAllBytes("foto.jpg"),
    MimeType = "image/jpeg"
};
var resposta = await client.Messages.SendMedia("id_da_instancia", mediaRequest);
```

### Exemplo: Listar grupos
```csharp
var grupos = await client.Group.ListGroups("id_da_instancia");
foreach (var grupo in grupos.Groups)
{
    Console.WriteLine($"Grupo: {grupo.Name} - ID: {grupo.Id}");
}
```

### Exemplo: Enviar mensagem de texto para grupo
```csharp
var textoGrupo = new RequestGroupTextMessage
{
    GroupId = "id_do_grupo",
    Text = "Olá, grupo!"
};
var respostaTexto = await client.Group.SendText("id_da_instancia", textoGrupo);
```

### Exemplo: Enviar mídia para grupo
```csharp
var mediaGrupo = new RequestGroupMediaMessage
{
    GroupId = "id_do_grupo",
    Type = "image",
    Caption = "Foto do grupo",
    FileName = "foto.jpg",
    FileBytes = File.ReadAllBytes("foto.jpg"),
    MimeType = "image/jpeg"
};
var respostaMedia = await client.Group.SendMedia("id_da_instancia", mediaGrupo);
```

### Exemplo: Configurar webhook para receber notificações
```csharp
var webhookRequest = new ConfigureWebhookRequest
{
    Url = "https://seuservidor.com/webhook",
    Events = new List<string> { "MESSAGES_UPSERT", "CONNECTION_UPDATE" }
};
await client.Instances.ConfigureWebhook(webhookRequest);
```

### Exemplo: Modelo de evento recebido (mensagem)
```csharp
public class MeuWebhookController : ControllerBase
{
    [HttpPost("/webhook")]
    public IActionResult ReceberEvento([FromBody] WebhookEventMessage evento)
    {
        Console.WriteLine($"Mensagem recebida: {evento.Message.Conversation}");
        return Ok();
    }
}
```

### Exemplo: Evento de status de conexão (CONNECTION_UPDATE)
```csharp
[HttpPost("/webhook/connection")]
public IActionResult ReceberStatus([FromBody] WebhookEventConnectionUpdate evento)
{
    Console.WriteLine($"Instância: {evento.Instance} - Status: {evento.State}");
    return Ok();
}

### Exemplo: Evento de QR Code (QRCODE_UPDATED)
```csharp
[HttpPost("/webhook/qrcode")]
public IActionResult ReceberQrCode([FromBody] WebhookEventQrCodeUpdated evento)
{
    Console.WriteLine($"Instância: {evento.Instance} - QRCode (base64): {evento.QrCode}");
    return Ok();
}
```
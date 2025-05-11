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

### Exemplo: Enviar mensagem com botões
```csharp
var buttonRequest = new RequestButtonMessage
{
    Number = "5511999999999",
    Text = "Escolha uma opção:",
    Buttons = new List<Button>
    {
        new Button { Id = "1", Text = "Opção 1" },
        new Button { Id = "2", Text = "Opção 2" },
        new Button { Id = "3", Text = "Opção 3" }
    }
};
var resposta = await client.Messages.SendButton("id_da_instancia", buttonRequest);
```

### Exemplo: Enviar enquete (Poll)
```csharp
var pollRequest = new RequestPollMessage
{
    Number = "5511999999999",
    Question = "Qual sua cor favorita?",
    Options = new List<string> { "Azul", "Verde", "Vermelho" }
};
var resposta = await client.Messages.SendPoll("id_da_instancia", pollRequest);
```

### Exemplo: Enviar mensagem de lista
```csharp
var listRequest = new RequestListMessage
{
    Number = "5511999999999",
    Title = "Escolha um item:",
    Description = "Selecione uma das opções abaixo:",
    Items = new List<ListItem>
    {
        new ListItem { Id = "1", Text = "Item 1" },
        new ListItem { Id = "2", Text = "Item 2" }
    }
};
var resposta = await client.Messages.SendList("id_da_instancia", listRequest);
```

### Exemplo: Enviar status
```csharp
var statusRequest = new RequestStatusMessage
{
    Instance = "id_da_instancia",
    Status = "Novo status do WhatsApp!"
};
var resposta = await client.Messages.SendStatus("id_da_instancia", statusRequest);
```

### Exemplo: Enviar localização
```csharp
var locationRequest = new RequestLocationMessage
{
    Number = "5511999999999",
    Latitude = -23.55052,
    Longitude = -46.633308,
    Name = "Praça da Sé",
    Address = "São Paulo, SP"
};
var resposta = await client.Messages.SendLocation("id_da_instancia", locationRequest);
```

### Exemplo: Enviar contato
```csharp
var contactRequest = new RequestContactMessage
{
    Number = "5511999999999",
    ContactName = "João Silva",
    ContactNumber = "5511988888888",
    Email = "joao@email.com"
};
var resposta = await client.Messages.SendContact("id_da_instancia", contactRequest);
```

### Exemplo: Enviar reação
```csharp
var reactionRequest = new RequestReactionMessage
{
    Number = "5511999999999",
    MessageId = "id_da_mensagem",
    Emoji = "👍"
};
var resposta = await client.Messages.SendReaction("id_da_instancia", reactionRequest);
```

### Exemplo: Enviar sticker
```csharp
var stickerRequest = new RequestStickerMessage
{
    Number = "5511999999999",
    FileName = "sticker.webp",
    FileBytes = File.ReadAllBytes("sticker.webp"),
    MimeType = "image/webp"
};
var resposta = await client.Messages.SendSticker("id_da_instancia", stickerRequest);
```

### Exemplo: Enviar áudio do WhatsApp
```csharp
var audioRequest = new RequestAudioMessage
{
    Number = "5511999999999",
    FileName = "audio.ogg",
    FileBytes = File.ReadAllBytes("audio.ogg"),
    MimeType = "audio/ogg"
};
var resposta = await client.Messages.SendAudio("id_da_instancia", audioRequest);
```

### Exemplo: Receber eventos via Webhook

```csharp
[ApiController]
[Route("webhook")]
public class WebhookController : ControllerBase
{
    [HttpPost("poll")]
    public IActionResult ReceberPoll([FromBody] RequestPollMessage poll)
    {
        Console.WriteLine($"Enquete recebida: {poll.Question} - Opções: {string.Join(", ", poll.Options)}");
        return Ok();
    }

    [HttpPost("list")]
    public IActionResult ReceberList([FromBody] RequestListMessage list)
    {
        Console.WriteLine($"Lista recebida: {list.Title} - Itens: {string.Join(", ", list.Items.Select(i => i.Text))}");
        return Ok();
    }

    [HttpPost("status")]
    public IActionResult ReceberStatus([FromBody] RequestStatusMessage status)
    {
        Console.WriteLine($"Status recebido: {status.Status}");
        return Ok();
    }

    [HttpPost("location")]
    public IActionResult ReceberLocation([FromBody] RequestLocationMessage location)
    {
        Console.WriteLine($"Localização recebida: {location.Name} ({location.Latitude}, {location.Longitude})");
        return Ok();
    }

    [HttpPost("contact")]
    public IActionResult ReceberContact([FromBody] RequestContactMessage contact)
    {
        Console.WriteLine($"Contato recebido: {contact.ContactName} - {contact.ContactNumber}");
        return Ok();
    }

    [HttpPost("reaction")]
    public IActionResult ReceberReaction([FromBody] RequestReactionMessage reaction)
    {
        Console.WriteLine($"Reação recebida: {reaction.Emoji} para mensagem {reaction.MessageId}");
        return Ok();
    }

    [HttpPost("sticker")]
    public IActionResult ReceberSticker([FromBody] RequestStickerMessage sticker)
    {
        Console.WriteLine($"Sticker recebido para: {sticker.Number}");
        return Ok();
    }

    [HttpPost("audio")]
    public IActionResult ReceberAudio([FromBody] RequestAudioMessage audio)
    {
        Console.WriteLine($"Áudio recebido para: {audio.Number}");
        return Ok();
    }
}
```
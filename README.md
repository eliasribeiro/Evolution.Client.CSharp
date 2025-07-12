# Evolution.Client.CSharp

SDK em C# para integração com a **Evolution API v2.2**. Esta biblioteca oferece uma interface robusta, assíncrona e tipada para interagir com todos os recursos da Evolution API, eliminando a necessidade de lidar diretamente com requisições HTTP e serialização manual.

## 🚀 Características

- ✅ **Totalmente assíncrono** - Suporte completo para async/await
- ✅ **Fortemente tipado** - IntelliSense completo e verificação de tipos em tempo de compilação
- ✅ **Thread-safe** - Instâncias reutilizáveis em cenários paralelos
- ✅ **Tratamento de erros robusto** - Exceções específicas com informações detalhadas
- ✅ **Cobertura completa** - Suporte a todos os módulos da Evolution API v2.2
- ✅ **Testes abrangentes** - Cobertura de testes.

## 📦 Instalação

```bash
dotnet add package Evolution.Client.CSharp
```

## 🔧 Uso Básico

### Configuração do Cliente

```csharp
using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Models;
using Evolution.Client.CSharp.Modules;

// Configuração simples
var client = EvolutionClient.Create("https://api.evolution.com", "sua-api-key");

// Configuração avançada
var client = EvolutionClient.Create("https://api.evolution.com", "sua-api-key", options =>
{
    options.Timeout = TimeSpan.FromMinutes(2);
    options.LogHttpRequests = true;
});
```

### Informações da API

```csharp
// Obter informações da Evolution API
var apiInfo = await client.GetInformationAsync();
Console.WriteLine($"API Version: {apiInfo.Version}");
Console.WriteLine($"Status: {apiInfo.Status}");
Console.WriteLine($"Documentation: {apiInfo.Documentation}");
```

### Gerenciamento de Instâncias

```csharp
// Criar uma nova instância
var createRequest = new CreateInstanceRequest
{
    InstanceName = "minha-instancia",
    Number = "5511999999999",
    Qrcode = true,
    Integration = "WHATSAPP-BAILEYS",
    RejectCall = false,
    AlwaysOnline = true,
    ReadMessages = true,
    Webhook = new WebhookSettings
    {
        Url = "https://meusite.com/webhook",
        ByEvents = true,
        Events = new[] { "MESSAGE_RECEIVED", "CONNECTION_UPDATE" }
    }
};

var instance = await client.Instances.CreateAsync(createRequest);

// Conectar instância
var connection = await client.Instances.ConnectAsync("minha-instancia");
Console.WriteLine($"QR Code: {connection.Base64}");

// Verificar status de conexão
var status = await client.Instances.GetConnectionStatusAsync("minha-instancia");
Console.WriteLine($"Estado: {status.State}");

// Definir presença
var presenceRequest = new SetPresenceRequest
{
    Presence = "available" // available, unavailable, composing, recording, paused
};
await client.Instances.SetPresenceAsync("minha-instancia", presenceRequest);

// Listar todas as instâncias
var instances = await client.Instances.ListAsync();

// Reiniciar instância
await client.Instances.RestartAsync("minha-instancia");

// Desconectar instância
await client.Instances.DisconnectAsync("minha-instancia");

// Deletar instância
await client.Instances.DeleteAsync("minha-instancia");
```

### Configuração de Webhook

```csharp
// Configurar webhook
var webhookRequest = new SetWebhookRequest
{
    Enabled = true,
    Url = "https://meusite.com/webhook",
    WebhookByEvents = true,
    WebhookBase64 = true,
    Events = new[]
    {
        "MESSAGE_RECEIVED",
        "CONNECTION_UPDATE",
        "QRCODE_UPDATED",
        "STATUS_INSTANCE",
        "MESSAGES_UPSERT",
        "CONTACTS_UPDATE",
        "PRESENCE_UPDATE",
        "CHATS_UPDATE",
        "GROUPS_UPSERT"
    }
};

var webhookResponse = await client.Webhooks.SetAsync("minha-instancia", webhookRequest);
Console.WriteLine($"Webhook configurado: {webhookResponse.Webhook?.Webhook?.Url}");

// Obter configuração do webhook
var webhookConfig = await client.Webhooks.GetAsync("minha-instancia");
Console.WriteLine($"URL do webhook: {webhookConfig.Url}");
```

### Configurações da Instância

```csharp
// Configurar settings
var settingsRequest = new SetSettingsRequest
{
    RejectCall = true,
    MsgCall = "Chamada rejeitada automaticamente",
    GroupsIgnore = false,
    AlwaysOnline = true,
    ReadMessages = true,
    ReadStatus = false,
    SyncFullHistory = true
};

var settingsResponse = await client.Settings.SetAsync("minha-instancia", settingsRequest);
Console.WriteLine($"Settings configurados para: {settingsResponse.Settings?.InstanceName}");

// Obter configurações
var settings = await client.Settings.GetAsync("minha-instancia");
Console.WriteLine($"Sempre online: {settings.AlwaysOnline}");
```

### Gerenciamento de Perfil

```csharp
// Buscar perfil do usuário
var fetchProfileRequest = new FetchProfileRequest
{
    Number = "5511999999999"
};
var userProfile = await client.Profile.FetchProfileAsync("minha-instancia", fetchProfileRequest);
Console.WriteLine($"Nome do usuário: {userProfile.Name}");

// Buscar perfil de negócio
var fetchBusinessRequest = new FetchBusinessProfileRequest
{
    Number = "5511888888888"
};
var businessProfile = await client.Profile.FetchBusinessProfileAsync("minha-instancia", fetchBusinessRequest);
Console.WriteLine($"Nome do negócio: {businessProfile.Name}");

// Atualizar nome do perfil
var updateNameRequest = new UpdateProfileNameRequest
{
    Name = "Meu Bot WhatsApp"
};
await client.Profile.UpdateProfileNameAsync("minha-instancia", updateNameRequest);

// Atualizar status do perfil
var updateStatusRequest = new UpdateProfileStatusRequest
{
    Status = "Disponível 24/7 para atendimento"
};
await client.Profile.UpdateProfileStatusAsync("minha-instancia", updateStatusRequest);

// Atualizar foto do perfil
var updatePictureRequest = new UpdateProfilePictureRequest
{
    Picture = "https://example.com/profile-picture.jpg"
};
await client.Profile.UpdateProfilePictureAsync("minha-instancia", updatePictureRequest);

// Remover foto do perfil
await client.Profile.RemoveProfilePictureAsync("minha-instancia");

// Buscar configurações de privacidade
var privacySettings = await client.Profile.FetchPrivacySettingsAsync("minha-instancia");
Console.WriteLine($"Configuração de leitura: {privacySettings.ReadReceipts}");

// Atualizar configurações de privacidade
var updatePrivacyRequest = new UpdatePrivacySettingsRequest
{
    ReadReceipts = "all",      // all, contacts, none
    Profile = "contacts",      // all, contacts, none
    Status = "contacts",       // all, contacts, none
    Online = "all",           // all, contacts, none
    GroupAdd = "contacts",    // all, contacts, none
    CallAdd = "all"           // all, contacts, none
};
await client.Profile.UpdatePrivacySettingsAsync("minha-instancia", updatePrivacyRequest);
```

### Envio de Mensagens

```csharp
// Mensagem de texto
var textMessage = new SendTextMessageRequest
{
    Number = "5511888888888",
    Text = "Olá! Como você está?"
};

var response = await client.Messages.SendTextAsync("minha-instancia", textMessage);

// Mensagem de mídia
var mediaMessage = new SendMediaMessageRequest
{
    Number = "5511888888888",
    MediaUrl = "https://exemplo.com/imagem.jpg",
    MediaType = "image",
    Caption = "Confira esta imagem!"
};

await client.Messages.SendMediaAsync("minha-instancia", mediaMessage);

// Mensagem de localização
var locationMessage = new SendLocationMessageRequest
{
    Number = "5511888888888",
    Latitude = -23.5505,
    Longitude = -46.6333,
    Name = "São Paulo",
    Address = "São Paulo, SP, Brasil"
};

await client.Messages.SendLocationAsync("minha-instancia", locationMessage);
```

### Gerenciamento de Grupos

```csharp
// Criar grupo
var groupRequest = new CreateGroupRequest
{
    Subject = "Meu Grupo",
    Description = "Descrição do grupo",
    Participants = new[] { "5511111111111", "5511222222222" }
};

var group = await client.Groups.CreateAsync("minha-instancia", groupRequest);

// Atualizar foto do grupo
var pictureRequest = new UpdateGroupPictureRequest
{
    Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQ..."
};
await client.Groups.UpdateGroupPictureAsync("minha-instancia", group.Id, pictureRequest);

// Adicionar participantes
var addParticipants = new UpdateParticipantRequest
{
    Action = "add",
    Participants = new[] { "5511333333333" }
};
await client.Groups.UpdateParticipantAsync("minha-instancia", group.Id, addParticipants);

// Obter código de convite
var inviteCode = await client.Groups.FetchInviteCodeAsync("minha-instancia", group.Id);
Console.WriteLine($"Link do grupo: {inviteCode.InviteUrl}");

// Listar todos os grupos
var groups = await client.Groups.FetchAllGroupsAsync("minha-instancia", getParticipants: true);
foreach (var g in groups)
{
    Console.WriteLine($"Grupo: {g.Subject} - Participantes: {g.Size}");
}

// Configurar apenas admins podem enviar mensagens
var settingRequest = new UpdateGroupSettingRequest
{
    Action = "announcement"
};
await client.Groups.UpdateSettingAsync("minha-instancia", group.Id, settingRequest);

// Ativar mensagens efêmeras (24 horas)
var ephemeralRequest = new ToggleEphemeralRequest
{
    Expiration = 86400 // 24 horas em segundos
};
await client.Groups.ToggleEphemeralAsync("minha-instancia", group.Id, ephemeralRequest);
```

### Configuração de Webhooks

```csharp
// Configurar webhook
var webhookRequest = new SetWebhookRequest
{
    Url = "https://meusite.com/webhook",
    Events = new[] { "message", "status", "connection" }
};

await client.Webhooks.SetAsync("minha-instancia", webhookRequest);
```

## 🏗️ Estrutura do Projeto

```
src/
 └ Evolution.Client/
     ├ Core/Http/          # Serviços HTTP e tratamento de erros
     ├ Modules/            # Módulos da API (Instances, Messages, etc.)
     └ Models/             # Modelos de dados
tests/
 └ Evolution.Client.Tests/ # Testes unitários
samples/
 └ BasicUsage/            # Exemplos de uso
```

## 🧪 Executando os Testes

```bash
dotnet test
```

## 📋 Requisitos

- .NET 9.0 ou superior
- C# 12 ou superior

## 🤝 Contribuindo

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## 🔗 Links Úteis

- [Documentação da Evolution API](https://doc.evolution-api.com/v2/api-reference/get-information)
- [Exemplos de Uso](./samples/)
- [Changelog](./CHANGELOG.md)

## 📞 Suporte

Para suporte e dúvidas:
- Abra uma [issue](https://github.com/EvolutionAPI/Evolution.Client.CSharp/issues)
- Consulte a [documentação oficial](https://doc.evolution-api.com/v2/api-reference/get-information)

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
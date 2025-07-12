# Evolution API - Funcionalidades de Mensagens

Este documento descreve as funcionalidades de envio de mensagens implementadas no Evolution Client C#.

## Funcionalidades Implementadas

### 1. Envio de Texto
Envia mensagens de texto simples com suporte a:
- Preview de links
- Menções (@everyone ou usuários específicos)
- Mensagens citadas
- Delay personalizado

```csharp
var request = new SendTextMessageRequest
{
    Number = "5511999999999",
    Text = "Olá! Esta é uma mensagem de texto.",
    LinkPreview = true,
    Delay = 1000
};

var response = await client.Messages.SendTextAsync("instance-name", request);
```

### 2. Envio de Mídia
Envia imagens, vídeos, documentos e outros tipos de mídia:
- Suporte a URLs ou base64
- Legendas personalizadas
- Tipos MIME específicos
- Nome de arquivo personalizado

```csharp
var request = new SendMediaMessageRequest
{
    Number = "5511999999999",
    MediaType = "image",
    MimeType = "image/jpeg",
    Media = "https://example.com/image.jpg",
    Caption = "Confira esta imagem!"
};

var response = await client.Messages.SendMediaAsync("instance-name", request);
```

### 3. Envio de Áudio
Envia mensagens de áudio ou notas de voz:

```csharp
var request = new SendAudioMessageRequest
{
    Number = "5511999999999",
    Audio = "https://example.com/audio.mp3"
};

var response = await client.Messages.SendAudioAsync("instance-name", request);
```

### 4. Envio de Localização
Compartilha localizações geográficas:

```csharp
var request = new SendLocationMessageRequest
{
    Number = "5511999999999",
    Latitude = -23.5505,
    Longitude = -46.6333,
    Name = "São Paulo",
    Address = "São Paulo, SP, Brasil"
};

var response = await client.Messages.SendLocationAsync("instance-name", request);
```

### 5. Envio de Status
Publica status no WhatsApp:

```csharp
var request = new SendStatusMessageRequest
{
    Type = "text",
    Content = "Meu status personalizado!",
    BackgroundColor = "#FF5722",
    FontColor = "#FFFFFF"
};

var response = await client.Messages.SendStatusAsync("instance-name", request);
```

### 6. Envio de Stickers
Envia stickers animados ou estáticos:

```csharp
var request = new SendStickerMessageRequest
{
    Number = "5511999999999",
    Sticker = "https://example.com/sticker.webp"
};

var response = await client.Messages.SendStickerAsync("instance-name", request);
```

### 7. Envio de Contatos
Compartilha informações de contato:

```csharp
var request = new SendContactMessageRequest
{
    Number = "5511999999999",
    Contacts = new[]
    {
        new MessageContactInfo
        {
            FullName = "João Silva",
            PhoneNumbers = new[]
            {
                new PhoneNumber { Number = "5511888888888", Type = "MOBILE" }
            }
        }
    }
};

var response = await client.Messages.SendContactAsync("instance-name", request);
```

### 8. Envio de Reações
Adiciona reações emoji a mensagens:

```csharp
var request = new SendReactionMessageRequest
{
    Number = "5511999999999",
    Key = new MessageKey { Id = "message-id" },
    Reaction = "👍"
};

var response = await client.Messages.SendReactionAsync("instance-name", request);
```

### 9. Envio de Enquetes
Cria enquetes interativas:

```csharp
var request = new SendPollMessageRequest
{
    Number = "5511999999999",
    Name = "Qual sua cor favorita?",
    Options = new[]
    {
        new PollOption { OptionName = "Azul" },
        new PollOption { OptionName = "Vermelho" }
    },
    SelectableOptionsCount = 1
};

var response = await client.Messages.SendPollAsync("instance-name", request);
```

### 10. Envio de Listas
Cria listas interativas com seções:

```csharp
var request = new SendListMessageRequest
{
    Number = "5511999999999",
    Title = "Menu do Restaurante",
    Description = "Escolha uma opção",
    ButtonText = "Ver Menu",
    Sections = new[]
    {
        new ListSection
        {
            Title = "Pratos Principais",
            Rows = new[]
            {
                new ListItem { Id = "1", Title = "Pizza", Description = "Pizza Margherita" }
            }
        }
    }
};

var response = await client.Messages.SendListAsync("instance-name", request);
```

### 11. Envio de Botões
Cria mensagens com botões interativos:

```csharp
var request = new SendButtonMessageRequest
{
    Number = "5511999999999",
    Title = "Confirmação",
    Description = "Deseja confirmar?",
    Buttons = new[]
    {
        new MessageButton { Id = "yes", Title = "Sim", DisplayText = "✅ Confirmar" },
        new MessageButton { Id = "no", Title = "Não", DisplayText = "❌ Cancelar" }
    }
};

var response = await client.Messages.SendButtonAsync("instance-name", request);
```

## Endpoints da API

| Funcionalidade | Endpoint | Método |
|---------------|----------|---------|
| Texto | `/message/sendText/{instance}` | POST |
| Status | `/message/sendStatus/{instance}` | POST |
| Mídia | `/message/sendMedia/{instance}` | POST |
| Áudio | `/message/sendAudio/{instance}` | POST |
| Sticker | `/message/sendSticker/{instance}` | POST |
| Localização | `/message/sendLocation/{instance}` | POST |
| Contato | `/message/sendContact/{instance}` | POST |
| Reação | `/message/sendReaction/{instance}` | POST |
| Enquete | `/message/sendPoll/{instance}` | POST |
| Lista | `/message/sendList/{instance}` | POST |
| Botões | `/message/sendButtons/{instance}` | POST |

## Tratamento de Erros

Todos os métodos podem lançar as seguintes exceções:
- `ArgumentNullException`: Quando parâmetros obrigatórios são nulos
- `ArgumentException`: Quando parâmetros têm valores inválidos
- `HttpRequestException`: Quando há problemas na comunicação com a API

## Testes

O projeto inclui testes unitários abrangentes para todas as funcionalidades implementadas. Execute os testes com:

```bash
dotnet test tests/Evolution.Client.Tests/Evolution.Client.Tests.csproj
```

## Exemplos Completos

Consulte o arquivo `examples/MessageExamples.cs` para exemplos detalhados de uso de todas as funcionalidades.

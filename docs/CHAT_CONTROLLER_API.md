# Evolution API - Chat Controller

Este documento descreve as funcionalidades do Chat Controller implementadas no Evolution Client C#.

## Funcionalidades Implementadas

### 1. Verificar Números no WhatsApp
Verifica se uma lista de números de telefone existe no WhatsApp.

```csharp
var request = new CheckWhatsAppNumbersRequest
{
    Numbers = new[] { "5511999999999", "5511888888888" }
};

var results = await client.Chat.CheckWhatsAppNumbersAsync("instance-name", request);
foreach (var result in results)
{
    Console.WriteLine($"Número: {result.Number}, Existe: {result.Exists}");
}
```

### 2. Marcar Mensagens como Lidas
Marca todas as mensagens de um chat como lidas.

```csharp
var request = new MarkAsReadChatRequest
{
    RemoteJid = "5511999999999@s.whatsapp.net",
    ReadMessages = true
};

var response = await client.Chat.MarkAsReadAsync("instance-name", request);
```

### 3. Marcar Mensagens como Não Lidas
Marca mensagens de um chat como não lidas.

```csharp
var request = new MarkAsUnreadChatRequest
{
    RemoteJid = "5511999999999@s.whatsapp.net"
};

var response = await client.Chat.MarkAsUnreadAsync("instance-name", request);
```

### 4. Arquivar/Desarquivar Chat
Arquiva ou desarquiva um chat específico.

```csharp
var request = new ArchiveChatRequest
{
    RemoteJid = "5511999999999@s.whatsapp.net",
    Archive = true // true para arquivar, false para desarquivar
};

var response = await client.Chat.ArchiveChatAsync("instance-name", request);
```

### 5. Deletar Mensagem para Todos
Remove uma mensagem para todos os participantes do chat.

```csharp
var request = new DeleteMessageForEveryoneRequest
{
    Key = new MessageKey
    {
        Id = "BAE5F5A632EAE722",
        RemoteJid = "5511999999999@s.whatsapp.net",
        FromMe = true
    }
};

var response = await client.Chat.DeleteMessageForEveryoneAsync("instance-name", request);
```

### 6. Atualizar Mensagem
Edita o texto de uma mensagem já enviada.

```csharp
var request = new UpdateMessageRequest
{
    Number = "5511999999999",
    Text = "Texto atualizado",
    Key = new MessageKey
    {
        Id = "BAE5F5A632EAE722",
        RemoteJid = "5511999999999@s.whatsapp.net",
        FromMe = true
    }
};

var response = await client.Chat.UpdateMessageAsync("instance-name", request);
```

### 7. Enviar Presença
Envia indicadores de presença (digitando, gravando, etc.).

```csharp
var request = new SendPresenceRequest
{
    Number = "5511999999999",
    Presence = "composing", // composing, recording, paused, available, unavailable
    Delay = 3000
};

var response = await client.Chat.SendPresenceAsync("instance-name", request);
```

### 8. Atualizar Status de Bloqueio
Bloqueia ou desbloqueia um contato.

```csharp
var request = new UpdateBlockStatusRequest
{
    Number = "5511999999999",
    Status = "block" // "block" ou "unblock"
};

var response = await client.Chat.UpdateBlockStatusAsync("instance-name", request);
```

### 9. Buscar URL da Foto do Perfil
Obtém a URL da foto do perfil de um contato.

```csharp
var request = new FetchProfilePictureRequest
{
    Number = "5511999999999"
};

var response = await client.Chat.FetchProfilePictureUrlAsync("instance-name", request);
Console.WriteLine($"URL: {response.ProfilePictureUrl}");
```

### 10. Obter Base64 de Mídia
Converte mídia de mensagens para base64.

```csharp
var request = new GetBase64Request
{
    Key = new MessageKey
    {
        Id = "BAE5F5A632EAE722",
        RemoteJid = "5511999999999@s.whatsapp.net"
    },
    ConvertToMp4 = false
};

var response = await client.Chat.GetBase64Async("instance-name", request);
```

### 11. Buscar Contatos
Busca contatos com filtros opcionais.

```csharp
var request = new FindContactsChatRequest
{
    Where = "João" // Termo de busca opcional
};

var contacts = await client.Chat.FindContactsAsync("instance-name", request);
```

### 12. Buscar Mensagens
Busca mensagens com filtros específicos.

```csharp
var request = new FindMessagesChatRequest
{
    Where = new MessageSearchWhere
    {
        RemoteJid = "5511999999999@s.whatsapp.net",
        FromMe = false
    },
    Limit = 10
};

var messages = await client.Chat.FindMessagesAsync("instance-name", request);
```

### 13. Buscar Mensagens de Status
Busca mensagens de status (stories).

```csharp
var request = new FindStatusMessageRequest
{
    Where = new StatusMessageWhere
    {
        Owner = "5511999999999@s.whatsapp.net"
    },
    Limit = 5
};

var statusMessages = await client.Chat.FindStatusMessagesAsync("instance-name", request);
```

### 14. Buscar Chats
Lista todos os chats com filtros opcionais.

```csharp
var request = new FindChatsRequest
{
    Where = new ChatSearchWhere
    {
        Archived = false
    },
    Limit = 20
};

var chats = await client.Chat.FindChatsAsync("instance-name", request);
```

## Endpoints da API

| Funcionalidade | Endpoint | Método |
|---------------|----------|---------|
| Verificar WhatsApp | `/chat/whatsappNumbers/{instance}` | POST |
| Marcar como Lida | `/chat/markMessageAsRead/{instance}` | POST |
| Marcar como Não Lida | `/chat/markMessageAsUnread/{instance}` | POST |
| Arquivar Chat | `/chat/archiveChat/{instance}` | POST |
| Deletar para Todos | `/chat/deleteMessageForEveryone/{instance}` | POST |
| Atualizar Mensagem | `/chat/updateMessage/{instance}` | POST |
| Enviar Presença | `/chat/sendPresence/{instance}` | POST |
| Atualizar Bloqueio | `/chat/updateBlockStatus/{instance}` | POST |
| Foto do Perfil | `/chat/fetchProfilePicture/{instance}` | POST |
| Obter Base64 | `/chat/getBase64FromMediaMessage/{instance}` | POST |
| Buscar Contatos | `/chat/findContacts/{instance}` | POST |
| Buscar Mensagens | `/chat/findMessages/{instance}` | POST |
| Buscar Status | `/chat/findStatusMessage/{instance}` | POST |
| Buscar Chats | `/chat/findChats/{instance}` | POST |

## Tipos de Presença

- `available` - Disponível
- `unavailable` - Indisponível
- `composing` - Digitando
- `recording` - Gravando áudio
- `paused` - Pausado

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

Consulte o arquivo `examples/ChatExamples.cs` para exemplos detalhados de uso de todas as funcionalidades.

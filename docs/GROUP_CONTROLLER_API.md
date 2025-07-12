# Evolution API - Group Controller

Este documento descreve as funcionalidades do Group Controller implementadas no Evolution Client C#.

## Funcionalidades Implementadas

### 1. Criar Grupo
Cria um novo grupo no WhatsApp.

```csharp
var request = new CreateGroupRequest
{
    Subject = "Meu Grupo",
    Description = "Descrição do grupo",
    Participants = new[] { "5511999999999", "5511888888888" }
};

var group = await client.Groups.CreateAsync("instance-name", request);
Console.WriteLine($"Grupo criado: {group.Subject} - ID: {group.Id}");
```

### 2. Atualizar Foto do Grupo
Atualiza a foto de perfil do grupo.

```csharp
var request = new UpdateGroupPictureRequest
{
    Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQ..." // Base64 da imagem
};

var response = await client.Groups.UpdateGroupPictureAsync("instance-name", "group-jid@g.us", request);
```

### 3. Atualizar Assunto do Grupo
Altera o nome/assunto do grupo.

```csharp
var request = new UpdateGroupSubjectRequest
{
    Subject = "Novo Nome do Grupo"
};

var response = await client.Groups.UpdateGroupSubjectAsync("instance-name", "group-jid@g.us", request);
```

### 4. Atualizar Descrição do Grupo
Modifica a descrição do grupo.

```csharp
var request = new UpdateGroupDescriptionRequest
{
    Description = "Nova descrição do grupo"
};

var response = await client.Groups.UpdateGroupDescriptionAsync("instance-name", "group-jid@g.us", request);
```

### 5. Obter Código de Convite
Busca o código de convite atual do grupo.

```csharp
var inviteCode = await client.Groups.FetchInviteCodeAsync("instance-name", "group-jid@g.us");
Console.WriteLine($"Código: {inviteCode.InviteCode}");
Console.WriteLine($"URL: {inviteCode.InviteUrl}");
```

### 6. Revogar Código de Convite
Gera um novo código de convite, invalidando o anterior.

```csharp
var newInviteCode = await client.Groups.RevokeInviteCodeAsync("instance-name", "group-jid@g.us");
Console.WriteLine($"Novo código: {newInviteCode.InviteCode}");
```

### 7. Enviar Convite do Grupo
Envia o link de convite para números específicos.

```csharp
var request = new SendGroupInviteRequest
{
    Numbers = new[] { "5511999999999", "5511888888888" },
    Text = "Venha participar do nosso grupo!"
};

var response = await client.Groups.SendGroupInviteAsync("instance-name", "group-jid@g.us", request);
```

### 8. Buscar Grupo por Código de Convite
Encontra informações de um grupo através do código de convite.

```csharp
var groupInfo = await client.Groups.FindGroupByInviteCodeAsync("instance-name", "ABC123DEF456");
if (groupInfo.Group != null)
{
    Console.WriteLine($"Grupo encontrado: {groupInfo.Group.Subject}");
}
```

### 9. Buscar Grupo por JID
Obtém informações detalhadas de um grupo específico.

```csharp
var group = await client.Groups.FindGroupByJidAsync("instance-name", "group-jid@g.us", getParticipants: true);
Console.WriteLine($"Grupo: {group.Subject}");
Console.WriteLine($"Participantes: {group.Size}");
```

### 10. Listar Todos os Grupos
Busca todos os grupos da instância.

```csharp
var groups = await client.Groups.FetchAllGroupsAsync("instance-name", getParticipants: false);
foreach (var group in groups)
{
    Console.WriteLine($"Grupo: {group.Subject} - Participantes: {group.Size}");
}
```

### 11. Buscar Participantes do Grupo
Lista todos os participantes de um grupo específico.

```csharp
var participants = await client.Groups.FindParticipantsAsync("instance-name", "group-jid@g.us");
foreach (var participant in participants)
{
    Console.WriteLine($"Participante: {participant.Number} - Admin: {participant.IsAdmin}");
}
```

### 12. Gerenciar Participantes
Adiciona, remove, promove ou rebaixa participantes do grupo.

```csharp
// Adicionar participantes
var addRequest = new UpdateParticipantRequest
{
    Action = "add",
    Participants = new[] { "5511999999999", "5511888888888" }
};
await client.Groups.UpdateParticipantAsync("instance-name", "group-jid@g.us", addRequest);

// Remover participantes
var removeRequest = new UpdateParticipantRequest
{
    Action = "remove",
    Participants = new[] { "5511999999999" }
};
await client.Groups.UpdateParticipantAsync("instance-name", "group-jid@g.us", removeRequest);

// Promover a admin
var promoteRequest = new UpdateParticipantRequest
{
    Action = "promote",
    Participants = new[] { "5511888888888" }
};
await client.Groups.UpdateParticipantAsync("instance-name", "group-jid@g.us", promoteRequest);

// Rebaixar de admin
var demoteRequest = new UpdateParticipantRequest
{
    Action = "demote",
    Participants = new[] { "5511888888888" }
};
await client.Groups.UpdateParticipantAsync("instance-name", "group-jid@g.us", demoteRequest);
```

### 13. Configurar Permissões do Grupo
Altera as configurações de permissões do grupo.

```csharp
// Apenas admins podem enviar mensagens
var announcementRequest = new UpdateGroupSettingRequest
{
    Action = "announcement"
};
await client.Groups.UpdateSettingAsync("instance-name", "group-jid@g.us", announcementRequest);

// Todos podem enviar mensagens
var notAnnouncementRequest = new UpdateGroupSettingRequest
{
    Action = "not_announcement"
};
await client.Groups.UpdateSettingAsync("instance-name", "group-jid@g.us", notAnnouncementRequest);

// Bloquear configurações do grupo
var lockedRequest = new UpdateGroupSettingRequest
{
    Action = "locked"
};
await client.Groups.UpdateSettingAsync("instance-name", "group-jid@g.us", lockedRequest);

// Desbloquear configurações do grupo
var unlockedRequest = new UpdateGroupSettingRequest
{
    Action = "unlocked"
};
await client.Groups.UpdateSettingAsync("instance-name", "group-jid@g.us", unlockedRequest);
```

### 14. Configurar Mensagens Efêmeras
Ativa ou desativa mensagens que desaparecem automaticamente.

```csharp
// Ativar mensagens efêmeras (24 horas)
var enableEphemeralRequest = new ToggleEphemeralRequest
{
    Expiration = 86400 // 24 horas em segundos
};
await client.Groups.ToggleEphemeralAsync("instance-name", "group-jid@g.us", enableEphemeralRequest);

// Desativar mensagens efêmeras
var disableEphemeralRequest = new ToggleEphemeralRequest
{
    Expiration = 0 // 0 para desativar
};
await client.Groups.ToggleEphemeralAsync("instance-name", "group-jid@g.us", disableEphemeralRequest);
```

### 15. Sair do Grupo
Remove a instância do grupo.

```csharp
var response = await client.Groups.LeaveGroupAsync("instance-name", "group-jid@g.us");
if (response.Success)
{
    Console.WriteLine("Saiu do grupo com sucesso");
}
```

## Endpoints da API

| Funcionalidade | Endpoint | Método |
|---------------|----------|---------|
| Criar Grupo | `/group/create/{instance}` | POST |
| Atualizar Foto | `/group/updateGroupPicture/{instance}` | POST |
| Atualizar Assunto | `/group/updateGroupSubject/{instance}` | POST |
| Atualizar Descrição | `/group/updateGroupDescription/{instance}` | POST |
| Obter Código de Convite | `/group/fetchInviteCode/{instance}` | GET |
| Revogar Código de Convite | `/group/revokeInviteCode/{instance}` | POST |
| Enviar Convite | `/group/sendInvite/{instance}` | POST |
| Buscar por Código | `/group/findGroupByInviteCode/{instance}` | GET |
| Buscar por JID | `/group/findGroupByJid/{instance}` | GET |
| Listar Grupos | `/group/fetchAllGroups/{instance}` | GET |
| Buscar Participantes | `/group/findParticipants/{instance}` | GET |
| Gerenciar Participantes | `/group/updateParticipant/{instance}` | POST |
| Configurar Permissões | `/group/updateSetting/{instance}` | POST |
| Mensagens Efêmeras | `/group/toggleEphemeral/{instance}` | POST |
| Sair do Grupo | `/group/leaveGroup/{instance}` | DELETE |

## Ações de Participantes

- `add` - Adicionar participantes
- `remove` - Remover participantes  
- `promote` - Promover a administrador
- `demote` - Rebaixar de administrador

## Configurações do Grupo

- `announcement` - Apenas admins podem enviar mensagens
- `not_announcement` - Todos podem enviar mensagens
- `locked` - Apenas admins podem alterar configurações
- `unlocked` - Todos podem alterar configurações

## Tratamento de Erros

Todos os métodos podem lançar as seguintes exceções:
- `ArgumentNullException`: Quando parâmetros obrigatórios são nulos
- `ArgumentException`: Quando parâmetros têm valores inválidos
- `HttpRequestException`: Quando há problemas na comunicação com a API

## Testes

O módulo possui testes unitários abrangentes que cobrem:
- Validação de parâmetros
- Chamadas corretas para a API
- Tratamento de respostas
- Cenários de erro

Execute os testes com:
```bash
dotnet test tests/Evolution.Client.Tests/Modules/GroupsModuleTests.cs
```

## Project Rules

1. O framework e versão.
    1. .NET 8.0
    2. System.Text.Json

## Project
O projeto é para desenvolver um SDK em C# para consumir a API EvolutionAPI, uma API para integração de mensagens com WhatsApp.

1. Documentação da API:
https://doc.evolution-api.com/v2/pt/get-started/introduction

2. Documentação dos Endpoints da API:
### Grupo API Informação:
- https://doc.evolution-api.com/v2/api-reference/get-information
### Instância:
- https://doc.evolution-api.com/v2/api-reference/instance-controller/create-instance-basic
- https://doc.evolution-api.com/v2/api-reference/instance-controller/fetch-instances
- https://doc.evolution-api.com/v2/api-reference/instance-controller/instance-connect
- https://doc.evolution-api.com/v2/api-reference/instance-controller/restart-instance
- https://doc.evolution-api.com/v2/api-reference/instance-controller/connection-state
- https://doc.evolution-api.com/v2/api-reference/instance-controller/logout-instance
- https://doc.evolution-api.com/v2/api-reference/instance-controller/delete-instance
-https://doc.evolution-api.com/v2/api-reference/instance-controller/set-presence

### Webhook
- https://doc.evolution-api.com/v2/api-reference/webhook/set
- https://doc.evolution-api.com/v2/api-reference/webhook/get

### Settings
- https://doc.evolution-api.com/v2/api-reference/settings/set
- https://doc.evolution-api.com/v2/api-reference/settings/get

### Send Message
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-text
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-status
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-media
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-audio
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-sticker
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-location
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-contact
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-reaction
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-poll
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-list
- https://doc.evolution-api.com/v2/api-reference/message-controller/send-button

### Chat Controller
- https://doc.evolution-api.com/v2/api-reference/chat-controller/check-is-whatsapp
- https://doc.evolution-api.com/v2/api-reference/chat-controller/mark-as-read
- https://doc.evolution-api.com/v2/api-reference/chat-controller/mark-as-unread
- https://doc.evolution-api.com/v2/api-reference/chat-controller/archive-chat
- https://doc.evolution-api.com/v2/api-reference/chat-controller/delete-message-for-everyone
- https://doc.evolution-api.com/v2/api-reference/chat-controller/update-message
- https://doc.evolution-api.com/v2/api-reference/chat-controller/send-presence
- https://doc.evolution-api.com/v2/api-reference/chat-controller/updateBlockStatus
- https://doc.evolution-api.com/v2/api-reference/chat-controller/fetch-profilepic-url
- https://doc.evolution-api.com/v2/api-reference/chat-controller/get-base64
- https://doc.evolution-api.com/v2/api-reference/chat-controller/find-contacts
- https://doc.evolution-api.com/v2/api-reference/chat-controller/find-messages
- https://doc.evolution-api.com/v2/api-reference/chat-controller/find-status-message
- https://doc.evolution-api.com/v2/api-reference/chat-controller/find-chats

### Profile Settings
- https://doc.evolution-api.com/v2/api-reference/profile-settings/fetch-business-profile
- https://doc.evolution-api.com/v2/api-reference/profile-settings/fetch-profile
- https://doc.evolution-api.com/v2/api-reference/profile-settings/update-profile-name
- https://doc.evolution-api.com/v2/api-reference/profile-settings/update-profile-status
- https://doc.evolution-api.com/v2/api-reference/profile-settings/update-profile-picture
- https://doc.evolution-api.com/v2/api-reference/profile-settings/remove-profile-picture
- https://doc.evolution-api.com/v2/api-reference/profile-settings/fetch-privacy-settings
- https://doc.evolution-api.com/v2/api-reference/profile-settings/update-privacy-settings

### Group Controller
- https://doc.evolution-api.com/v2/api-reference/group-controller/group-create
- https://doc.evolution-api.com/v2/api-reference/group-controller/update-group-picture
- https://doc.evolution-api.com/v2/api-reference/group-controller/update-group-subject
- https://doc.evolution-api.com/v2/api-reference/group-controller/update-group-description
- https://doc.evolution-api.com/v2/api-reference/group-controller/fetch-invite-code
- https://doc.evolution-api.com/v2/api-reference/group-controller/revoke-invite-code
- https://doc.evolution-api.com/v2/api-reference/group-controller/send-invite-url
- https://doc.evolution-api.com/v2/api-reference/group-controller/find-group-by-invite-code
- https://doc.evolution-api.com/v2/api-reference/group-controller/find-group-by-jid
- https://doc.evolution-api.com/v2/api-reference/group-controller/fetch-all-groups
- https://doc.evolution-api.com/v2/api-reference/group-controller/find-participants
- https://doc.evolution-api.com/v2/api-reference/group-controller/update-participant
- https://doc.evolution-api.com/v2/api-reference/group-controller/update-setting
- https://doc.evolution-api.com/v2/api-reference/group-controller/toggle-ephemeral
- https://doc.evolution-api.com/v2/api-reference/group-controller/leave-group

### WebSocket
- https://doc.evolution-api.com/v2/api-reference/integrations/websocket/set-websocket
- https://doc.evolution-api.com/v2/api-reference/integrations/websocket/find-websocket
using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp.Models.Webhook;
using Evolution.Client.CSharp.Models.Message.SendPoll;
using Evolution.Client.CSharp.Models.Message.SendList;
using Evolution.Client.CSharp.Models.Message.SendStatus;
using Evolution.Client.CSharp.Models.Message.SendLocation;
using Evolution.Client.CSharp.Models.Message.SendContact;
using Evolution.Client.CSharp.Models.Message.SendReaction;
using Evolution.Client.CSharp.Models.Message.SendSticker;
using Evolution.Client.CSharp.Models.Message.SendAudio;

namespace Evolution.Client.CSharp.Samples.Controllers
{
    [ApiController]
    [Route("webhook")]
    public class WebhookController : ControllerBase
    {
        [HttpPost("message")]
        public IActionResult ReceberMensagem([FromBody] WebhookEventMessage evento)
        {
            // Exemplo: logar mensagem recebida
            Console.WriteLine($"Mensagem recebida: {evento.Message.Conversation}");
            return Ok();
        }

        [HttpPost("connection")]
        public IActionResult ReceberStatus([FromBody] WebhookEventConnectionUpdate evento)
        {
            Console.WriteLine($"Instância: {evento.Instance} - Status: {evento.State}");
            return Ok();
        }

        [HttpPost("qrcode")]
        public IActionResult ReceberQrCode([FromBody] WebhookEventQrCodeUpdated evento)
        {
            Console.WriteLine($"Instância: {evento.Instance} - QRCode (base64): {evento.QrCode}");
            return Ok();
        }

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
} 
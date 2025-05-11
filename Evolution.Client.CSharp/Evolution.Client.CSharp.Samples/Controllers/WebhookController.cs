using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp.Models.Webhook;

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
    }
} 
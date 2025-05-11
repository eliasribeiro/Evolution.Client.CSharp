using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp.Models.Message.SendText;
using Evolution.Client.CSharp.Models.Message.SendMedia;
using Evolution.Client.CSharp.Models.Message.Group;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Evolution.Client.CSharp.Samples.Controllers
{
    public class MessageController : Controller
    {
        private EvolutionClient GetClient() => (EvolutionClient)HttpContext.RequestServices.GetService(typeof(EvolutionClient));

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendText(string instance, string number, string text)
        {
            var req = new RequestMessage { Number = number, Text = text };
            var resp = await GetClient().Messages.SendText(instance, req);
            return Json(resp);
        }

        [HttpPost]
        public async Task<IActionResult> SendMedia(string instance, string number, string type, string caption)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sample.jpg");
            var req = new RequestMediaMessage
            {
                Number = number,
                Type = type,
                Caption = caption,
                FileName = "sample.jpg",
                FileBytes = System.IO.File.ReadAllBytes(filePath),
                MimeType = "image/jpeg"
            };
            var resp = await GetClient().Messages.SendMedia(instance, req);
            return Json(resp);
        }

        [HttpPost]
        public async Task<IActionResult> SendGroupText(string instance, string groupId, string text)
        {
            var req = new RequestGroupTextMessage { GroupId = groupId, Text = text };
            var resp = await GetClient().Group.SendText(instance, req);
            return Json(resp);
        }

        [HttpPost]
        public async Task<IActionResult> SendGroupMedia(string instance, string groupId, string type, string caption)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sample.jpg");
            var req = new RequestGroupMediaMessage
            {
                GroupId = groupId,
                Type = type,
                Caption = caption,
                FileName = "sample.jpg",
                FileBytes = System.IO.File.ReadAllBytes(filePath),
                MimeType = "image/jpeg"
            };
            var resp = await GetClient().Group.SendMedia(instance, req);
            return Json(resp);
        }

        [HttpPost]
        public async Task<IActionResult> SendPoll(string instance, string number, string question, string options)
        {
            var req = new RequestPollMessage
            {
                Number = number,
                Question = question,
                Options = options.Split(',').Select(o => o.Trim()).ToList()
            };
            var resp = await GetClient().Messages.SendPoll(instance, req);
            ViewBag.Result = resp;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendList(string instance, string number, string title, string description, string items)
        {
            var req = new RequestListMessage
            {
                Number = number,
                Title = title,
                Description = description,
                Items = items.Split(',').Select((t, i) => new ListItem { Id = (i+1).ToString(), Text = t.Trim() }).ToList()
            };
            var resp = await GetClient().Messages.SendList(instance, req);
            ViewBag.Result = resp;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendStatus(string instance, string status)
        {
            var req = new RequestStatusMessage
            {
                Instance = instance,
                Status = status
            };
            var resp = await GetClient().Messages.SendStatus(instance, req);
            ViewBag.Result = resp;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendLocation(string instance, string number, double latitude, double longitude, string name, string address)
        {
            var req = new RequestLocationMessage
            {
                Number = number,
                Latitude = latitude,
                Longitude = longitude,
                Name = name,
                Address = address
            };
            var resp = await GetClient().Messages.SendLocation(instance, req);
            ViewBag.Result = resp;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendContact(string instance, string number, string contactName, string contactNumber, string email)
        {
            var req = new RequestContactMessage
            {
                Number = number,
                ContactName = contactName,
                ContactNumber = contactNumber,
                Email = email
            };
            var resp = await GetClient().Messages.SendContact(instance, req);
            ViewBag.Result = resp;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendReaction(string instance, string number, string messageId, string emoji)
        {
            var req = new RequestReactionMessage
            {
                Number = number,
                MessageId = messageId,
                Emoji = emoji
            };
            var resp = await GetClient().Messages.SendReaction(instance, req);
            ViewBag.Result = resp;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendSticker(string instance, string number)
        {
            var file = Request.Form.Files["stickerFile"];
            if (file == null) { ViewBag.Result = "Arquivo não enviado"; return View("Index"); }
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var req = new RequestStickerMessage
            {
                Number = number,
                FileName = file.FileName,
                FileBytes = ms.ToArray(),
                MimeType = file.ContentType
            };
            var resp = await GetClient().Messages.SendSticker(instance, req);
            ViewBag.Result = resp;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendAudio(string instance, string number)
        {
            var file = Request.Form.Files["audioFile"];
            if (file == null) { ViewBag.Result = "Arquivo não enviado"; return View("Index"); }
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var req = new RequestAudioMessage
            {
                Number = number,
                FileName = file.FileName,
                FileBytes = ms.ToArray(),
                MimeType = file.ContentType
            };
            var resp = await GetClient().Messages.SendAudio(instance, req);
            ViewBag.Result = resp;
            return View("Index");
        }
    }
}

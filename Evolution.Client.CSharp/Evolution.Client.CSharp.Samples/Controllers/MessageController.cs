using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp.Models.Message.SendText;
using Evolution.Client.CSharp.Models.Message.SendMedia;
using Evolution.Client.CSharp.Models.Message.Group;
using System.IO;
using System.Threading.Tasks;

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
    }
}

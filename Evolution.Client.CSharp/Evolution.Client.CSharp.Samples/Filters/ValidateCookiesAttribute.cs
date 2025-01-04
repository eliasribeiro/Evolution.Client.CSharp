using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Evolution.Client.CSharp.Samples.Filters;

public class ValidateCookiesAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Recupera os cookies da requisição
        var requestCookies = context.HttpContext.Request.Cookies;

        // Verifica se os cookies "ServerUrl" e "ApiKey" existem
        if (!requestCookies.ContainsKey("ServerUrl") || !requestCookies.ContainsKey("ApiKey"))
        {
            // Caso não existam, redireciona para Home/Index
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }

        base.OnActionExecuting(context);
    }
}
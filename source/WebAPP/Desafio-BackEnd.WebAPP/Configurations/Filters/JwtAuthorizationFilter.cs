using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Desafio_BackEnd.WebAPP.Configurations.Filters
{
    public class JwtAuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new BadRequestObjectResult("Token inválido");
                return;
            }

            context.ActionArguments["token"] = token;

            base.OnActionExecuting(context);
        }
    }
}
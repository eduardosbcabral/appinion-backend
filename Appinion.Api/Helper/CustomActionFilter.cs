using Appinion.ApplicationService.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Appinion.Api.Helper
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IUserHelper userHelper = context.HttpContext.RequestServices.GetService(typeof(IUserHelper)) as IUserHelper;

            IServiceContext serviceContext = context.HttpContext.RequestServices.GetService(typeof(IServiceContext)) as IServiceContext;
            serviceContext.AddParam("HOST CLIENTE", context.HttpContext.Request.Headers["Host"]);
            serviceContext.AddParam("BROWSER", context.HttpContext.Request.Headers["User-Agent"]);
            //serviceContext.AddParam("APLICAÇÃO DE ORIGEM", context.HttpContext.Request.Headers["Application-Key"]);

            if (!string.IsNullOrEmpty(userHelper.UsuarioAtual()))
            {
                serviceContext.SetUsuarioAtual(userHelper.UsuarioAtual());
            }

        }
    }
}

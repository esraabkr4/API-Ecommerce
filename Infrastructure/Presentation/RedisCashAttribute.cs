using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;

namespace Presentation
{
    public class RedisCashAttribute(int durationInSec) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CashService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cashService;
            string CashKey = GenerateCashKey(context.HttpContext.Request);
            var Result =await CashService.GetCashedValueAsync(CashKey);
            if (Result != null)
            {
                context.Result = new ContentResult
                {
                    Content = Result,
                    ContentType = "Application/Json",
                    StatusCode = (int)HttpStatusCode.OK,
                };
                return;
            }
            var ContextResult = await next.Invoke();
            if (ContextResult.Result is OkObjectResult okObject)
            {
                await CashService.SetCashedValueAsync(CashKey, okObject, TimeSpan.FromSeconds(durationInSec));
            }
        }
        private string GenerateCashKey(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                KeyBuilder.Append($"{item.Key} -- {item.Value}");

            }
            return KeyBuilder.ToString();
        }
    }
}
using System.Net;
using Contoso.Shop.Api.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Contoso.Shop.Api.Shared.Filters
{
    public class HandleErrorFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var friendlyErrorMessage = new ErrorResultDto
            {
                StackTrace = context.Exception.ToString(),
                Error = "Ops, ocorreu um erro interno!"
            };

            context.Result = new ObjectResult(friendlyErrorMessage)
            {
                StatusCode = (int?) HttpStatusCode.InternalServerError
            };
        }
    }
}
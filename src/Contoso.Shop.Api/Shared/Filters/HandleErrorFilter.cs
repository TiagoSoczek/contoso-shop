using System.Net;
using Contoso.Shop.Api.Shared.Dtos;
using Contoso.Shop.Api.Shared.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Contoso.Shop.Api.Shared.Filters
{
    public class HandleErrorFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public HandleErrorFilter(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public override void OnException(ExceptionContext context)
        {
            var stackTrace = hostingEnvironment.IsDevelopment() ? context.Exception.ToString() : null;

            var friendlyErrorMessage = new ErrorResultDto
            {
                StackTrace = stackTrace,
                Error = Messages.InternalServerError
            };

            context.Result = new ObjectResult(friendlyErrorMessage)
            {
                StatusCode = (int?) HttpStatusCode.InternalServerError
            };
        }
    }
}
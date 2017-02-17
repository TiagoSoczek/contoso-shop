using System.Collections.Generic;
using Contoso.Shop.Api.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Contoso.Shop.Api.Shared.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (KeyValuePair<string, object> actionArgumentsValue in filterContext.ActionArguments)
            {
                if (actionArgumentsValue.Value == null)
                {
                    // TODO: Improve error message
                    filterContext.ModelState.AddModelError(actionArgumentsValue.Key, "Null");
                }
            }

            if (filterContext.ModelState.IsValid)
            {
                return;
            }

            var issues = filterContext.ModelState.
                ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray());

            var resultDto = new ErrorResultDto
            {
                Error = "Falha de validação",
                Issues = issues
            };

            filterContext.Result = new BadRequestObjectResult(resultDto);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}
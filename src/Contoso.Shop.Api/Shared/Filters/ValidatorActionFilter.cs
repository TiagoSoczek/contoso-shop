using System.Collections.Generic;
using Contoso.Shop.Api.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Contoso.Shop.Api.Shared.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                    filterContext.ModelState.AddModelError(actionArgumentsValue.Key, Messages.NullParameterError);
                }
            }

            if (filterContext.ModelState.IsValid)
            {
                return;
            }

            var issues = filterContext.ModelState.
                ToDictionary(x => x.Key, x => x.Value.Errors.Select(GetValidationMessage).ToArray());

            var resultDto = new ErrorResultDto
            {
                Error = Messages.ValidationError,
                Issues = issues
            };

            filterContext.Result = new BadRequestObjectResult(resultDto);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        private string GetValidationMessage(ModelError error)
        {
            if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
            {
                return error.ErrorMessage;
            }

            if (error.Exception != null)
            {
                return error.Exception.Message;
            }

            return Messages.UnknownValidationError;
        }
    }
}
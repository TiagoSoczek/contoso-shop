using System;
using Contoso.Shop.Api.Shared.Dtos;
using Contoso.Shop.Model.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Shop.Api.Shared
{
    public abstract class BaseController : Controller
    {
        protected IActionResult As<T, TR>(Result<T> result, Func<T, TR> map)
        {
            if (result.IsSuccess)
            {
                if (map != null)
                {
                    return Ok(map(result.Value));
                }

                return Ok(result.Value);
            }

            return As((Result)result);
        }

        protected IActionResult As<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return As((Result)result);
        }

        protected IActionResult As(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok();
            }

            var errorResultDto = new ErrorResultDto
            {
                Error = result.Error
            };

            switch (result.Code)
            {
                case ResultCode.NotFound:
                    return NotFound(errorResultDto);
                case ResultCode.BadRequest:
                default:
                    return BadRequest(errorResultDto);
            }
        }
    }
}
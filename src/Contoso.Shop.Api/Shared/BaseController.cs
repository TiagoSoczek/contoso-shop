using Contoso.Shop.Api.Shared.Dtos;
using Contoso.Shop.Model.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Shop.Api.Shared
{
    public abstract class BaseController : Controller
    {
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
                case ResultCode.BadRequest:
                    return BadRequest(errorResultDto);
                case ResultCode.NotFound:
                    return NotFound(errorResultDto);
                default:
                    return StatusCode(500, errorResultDto);
            }
        }
    }
}
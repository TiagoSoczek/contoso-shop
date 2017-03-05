using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Contoso.Shop.Api.Shared.Dtos;
using Contoso.Shop.Model.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Shop.Api.Shared
{
    public abstract class BaseController : Controller
    {
        protected BaseController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }

        protected T MapTo<T>(object source) => Mapper.Map<T>(source);

        protected IActionResult As<T>(IEnumerable items)
        {
            return Ok(Mapper.Map<IEnumerable<T>>(items));
        }

        protected IActionResult As<T, TR>(Result<T> result, Func<object, TR> map)
        {
            if (result.IsSuccess)
            {
                return Ok(map(result.Value));
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
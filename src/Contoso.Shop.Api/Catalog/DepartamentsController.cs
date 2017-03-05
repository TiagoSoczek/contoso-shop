using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contoso.Shop.Api.Catalog.Dtos;
using Contoso.Shop.Api.Shared;
using Contoso.Shop.Model.Catalog;
using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;
using Contoso.Shop.Model.Shared.Commands;
using Contoso.Shop.Model.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Contoso.Shop.Api.Catalog
{
    [Route(RouteConstants.Controller)]
    public class DepartamentsController : BaseController
    {
        public DepartamentsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        [SwaggerResponse(200, typeof(DepartamentDto))]
        public async Task<IActionResult> Create([FromBody] CreateDepartament command)
        {
            Result<Departament> result = await Mediator.Send(command);

            return As(result, MapTo<DepartamentDto>);
        }

        [HttpGet]
        [SwaggerResponse(200, typeof(DepartamentDto[]))]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Departament> items = await Mediator.Send(Query.All<Departament>());

            return As<DepartamentDto>(items);
        }

        [HttpGet(RouteConstants.IdInt)]
        [SwaggerResponse(200, typeof(DepartamentDto))]
        public async Task<IActionResult> Get(int id)
        {
            Result<Departament> result = await Mediator.Send(Query.ById<Departament>(id));

            return As(result, MapTo<DepartamentDto>);
        }

        [HttpPost(RouteConstants.IdInt)]
        [SwaggerResponse(200, typeof(DepartamentDto))]
        public async Task<IActionResult> Update([FromBody] UpdateDepartament command, int id)
        {
            command.Id = id;

            Result<Departament> result = await Mediator.Send(command);

            return As(result, MapTo<DepartamentDto>);
        }

        [HttpDelete(RouteConstants.IdInt)]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await Mediator.Send(Remove.For<Departament>(id));

            return As(result);
        }
    }
}
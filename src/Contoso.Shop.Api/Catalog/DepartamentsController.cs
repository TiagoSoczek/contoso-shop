using System.Threading.Tasks;
using Contoso.Shop.Api.Catalog.Dtos;
using Contoso.Shop.Api.Shared;
using Contoso.Shop.Model.Catalog;
using Contoso.Shop.Model.Catalog.Handlers;
using Contoso.Shop.Model.Shared.Commands;
using Contoso.Shop.Model.Shared.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Contoso.Shop.Api.Catalog
{
    [Route(RouteConstants.Controller)]
    public class DepartamentsController : BaseController
    {
        private readonly DepartamentHandlers handler;

        public DepartamentsController(DepartamentHandlers handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        [SwaggerResponse(200, typeof(DepartamentDto))]
        public async Task<IActionResult> Create([FromBody] CreateDepartamentDto dto)
        {
            var result = await handler.Handle(dto);

            return As(result, MapToDto);
        }


        [HttpGet]
        [SwaggerResponse(200, typeof(DepartamentDto[]))]
        public async Task<IActionResult> Get()
        {
            var items = await handler.Handle(Query.All<Departament>());

            return Map(items, MapToDto);
        }

        [HttpGet(RouteConstants.IdInt)]
        [SwaggerResponse(200, typeof(DepartamentDto))]
        public async Task<IActionResult> Get(int id)
        {
            var result = await handler.Handle(Query.ById<Departament>(id));

            return As(result, MapToDto);
        }

        [HttpPost(RouteConstants.IdInt)]
        [SwaggerResponse(200, typeof(DepartamentDto))]
        public async Task<IActionResult> Update([FromBody] UpdateDepartamentDto dto, int id)
        {
            dto.Id = id;

            var result = await handler.Handle(dto);

            return As(result, MapToDto);
        }

        [HttpDelete(RouteConstants.IdInt)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await handler.Handle(Remove.For<Departament>(id));

            return As(result);
        }

        private DepartamentDto MapToDto(Departament departament)
        {
            return new DepartamentDto
            {
                Id = departament.Id,
                Title = departament.Title,
                Description = departament.Description,
                CreatedAt = departament.CreatedAt,
                UpdatedAt = departament.UpdatedAt
            };
        }
    }
}
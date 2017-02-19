using System.Threading.Tasks;
using Contoso.Shop.Api.Catalog.Dtos;
using Contoso.Shop.Api.Shared;
using Contoso.Shop.Model.Catalog;
using Contoso.Shop.Model.Catalog.Handlers;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create([FromBody] CreateDepartamentDto dto)
        {
            var result = await handler.Handle(dto);

            return As(result, MapToDto);
        }

        public DepartamentDto MapToDto(Departament departament)
        {
            return new DepartamentDto
            {
                Id = departament.Id,
                Title = departament.Title,
                Description = departament.Description,
            };
        }
    }
}
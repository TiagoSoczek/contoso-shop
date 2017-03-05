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

namespace Contoso.Shop.Api.Catalog
{
    [Route(RouteConstants.Controller)]
    public class ProductsController : BaseController
    {
        public ProductsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 200)]
        public async Task<IActionResult> Create([FromBody] CreateProduct command)
        {
            Result<Product> result = await Mediator.Send(command);

            return As(result, MapTo<ProductDto>);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductDto[]), 200)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Product> items = await Mediator.Send(Query.All<Product>());

            return As<ProductDto>(items);
        }

        [HttpGet(RouteConstants.IdInt)]
        [ProducesResponseType(typeof(ProductDto), 200)]
        public async Task<IActionResult> Get(int id)
        {
            Result<Product> result = await Mediator.Send(Query.ById<Product>(id));

            return As(result, MapTo<ProductDto>);
        }

        [HttpPost(RouteConstants.IdInt)]
        [ProducesResponseType(typeof(ProductDto), 200)]
        public async Task<IActionResult> Update([FromBody] UpdateProduct command, int id)
        {
            command.Id = id;

            Result<Product> result = await Mediator.Send(command);

            return As(result, MapTo<ProductDto>);
        }

        [HttpDelete(RouteConstants.IdInt)]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await Mediator.Send(Remove.For<Product>(id));

            return As(result);
        }
    }
}
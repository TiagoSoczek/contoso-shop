using System.Threading.Tasks;
using Contoso.Shop.Api.Catalog.Dtos;
using Contoso.Shop.Api.Shared;
using Contoso.Shop.Model.Catalog;
using Contoso.Shop.Model.Catalog.Handlers;
using Contoso.Shop.Model.Shared.Commands;
using Contoso.Shop.Model.Shared.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Shop.Api.Catalog
{
    [Route(RouteConstants.Controller)]
    public class ProductsController : BaseController
    {
        private readonly ProductHandlers handler;

        public ProductsController(ProductHandlers handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var result = await handler.Handle(dto);

            return As(result, MapToDto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await handler.Handle(Query.All<Product>());

            return Map(items, MapToDto);
        }

        [HttpGet(RouteConstants.IdInt)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await handler.Handle(Query.ById<Product>(id));

            return As(result, MapToDto);
        }

        [HttpPost(RouteConstants.IdInt)]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto dto, int id)
        {
            dto.Id = id;

            var result = await handler.Handle(dto);

            return As(result, MapToDto);
        }

        [HttpDelete(RouteConstants.IdInt)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await handler.Handle(Remove.For<Product>(id));

            return As(result);
        }

        public ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Sku = product.Sku,
                Title = product.Title,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                Quantity = product.Quantity,
                DepartamentId = product.DepartamentId,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
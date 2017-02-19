using System.Threading.Tasks;
using Contoso.Shop.Api.Catalog.Dtos;
using Contoso.Shop.Api.Shared;
using Contoso.Shop.Model.Catalog;
using Contoso.Shop.Model.Catalog.Handlers;
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
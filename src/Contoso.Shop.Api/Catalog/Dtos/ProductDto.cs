using System;

namespace Contoso.Shop.Api.Catalog.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int DepartamentId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
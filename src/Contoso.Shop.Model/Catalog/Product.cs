using System;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public class Product : Entity
    {
        public Sku Sku { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int DepartamentId { get; set; }

        public Departament Departament { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
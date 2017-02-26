using System;
using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public class Product : Entity
    {
        private Product()
        {
        }

        public string Sku { get; private set; }

        public string Title { get; private set; }

        public string ShortDescription { get; private set; }

        public decimal Price { get; private set; }

        public int Quantity { get; private set; }

        public int DepartamentId { get; private set; }

        public Departament Departament { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Product Create(ICreateProduct command)
        {
            return new Product
            {
                Sku = command.Sku,
                DepartamentId = command.DepartamentId,
                Price = command.Price,
                Quantity = command.Quantity,
                ShortDescription = command.ShortDescription,
                Title = command.Title,
                CreatedAt = Clock.Now
            };
        }

        public void Apply(IUpdateProduct command)
        {
            DepartamentId = command.DepartamentId;
            Price = command.Price;
            Quantity = command.Quantity;
            ShortDescription = command.ShortDescription;
            Title = command.Title;
            UpdatedAt = Clock.Now;
        }
    }
}
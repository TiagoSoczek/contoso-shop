using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public class Product : AuditedEntity
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

        public static Product Create(CreateProduct command)
        {
            return new Product
            {
                Sku = command.Sku,
                DepartamentId = command.DepartamentId,
                Price = command.Price,
                Quantity = command.Quantity,
                ShortDescription = command.ShortDescription,
                Title = command.Title
            };
        }

        public void Apply(UpdateProduct command)
        {
            DepartamentId = command.DepartamentId;
            Price = command.Price;
            Quantity = command.Quantity;
            ShortDescription = command.ShortDescription;
            Title = command.Title;
        }
    }
}
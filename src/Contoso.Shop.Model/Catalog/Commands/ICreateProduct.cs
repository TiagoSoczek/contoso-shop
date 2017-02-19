namespace Contoso.Shop.Model.Catalog.Commands
{
    public interface ICreateProduct
    {
        string Sku { get; }

        string Title { get; }

        string ShortDescription { get; }

        decimal? Price { get; }

        int? Quantity { get; }

        int? DepartamentId { get; }
    }
}
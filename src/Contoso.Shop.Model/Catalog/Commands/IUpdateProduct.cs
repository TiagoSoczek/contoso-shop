namespace Contoso.Shop.Model.Catalog.Commands
{
    public interface IUpdateProduct
    {
        int Id { get; }

        string Title { get; }

        string ShortDescription { get; }

        decimal Price { get; }

        int Quantity { get; }

        int DepartamentId { get; }
    }
}
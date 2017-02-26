namespace Contoso.Shop.Model.Catalog.Commands
{
    public interface IUpdateDepartament
    {
        int Id { get; }

        string Title { get; }

        string Description { get; }
    }
}
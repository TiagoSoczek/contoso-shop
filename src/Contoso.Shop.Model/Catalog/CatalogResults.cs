using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public static class CatalogResults
    {
        public static readonly Result ProductAlreadyExistsWithSku = Result.Fail("Produto já existe com o Sku informado");
    }
}
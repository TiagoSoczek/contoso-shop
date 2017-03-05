using Contoso.Shop.Model.Catalog.Resources;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public static class CatalogResults
    {
        public static readonly Result ProductAlreadyExistsWithSku = Result.Fail(Messages.ProductAlreadyExistsWithSku);
    }
}
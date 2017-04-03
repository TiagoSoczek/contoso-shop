using System.Collections.Generic;
using System.Threading.Tasks;
using Contoso.Shop.Infra.Shared.Data;
using Contoso.Shop.Infra.Shared.Repositories;
using Contoso.Shop.Model.Catalog;
using Contoso.Shop.Model.Catalog.Repositories;

namespace Contoso.Shop.Infra.Catalog.Repositories
{
    public class ProductRepository : EntityFrameworkRepository<Product>, IProductRepository
    {
        public ProductRepository(ShopDataContext context) : base(context)
        {
        }

        public Task<IEnumerable<Product>> GetProductsByDeptId(int deptId)
        {
            // SQL
            return Where(x => x.DepartamentId == deptId);
        }
    }
}
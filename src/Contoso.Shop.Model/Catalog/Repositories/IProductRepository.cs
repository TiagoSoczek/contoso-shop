using System.Collections.Generic;
using System.Threading.Tasks;
using Contoso.Shop.Model.Shared.Repositories;

namespace Contoso.Shop.Model.Catalog.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByDeptId(int deptId);
    }
}
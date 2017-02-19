using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Shop.Model.Shared.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task<bool> Exists(Func<T, bool> predicate);
    }
}
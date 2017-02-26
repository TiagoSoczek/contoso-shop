using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contoso.Shop.Model.Shared.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task Insert(IEnumerable<T> entities);

        Task<T> Insert(T entity);

        Task Update(IEnumerable<T> entities);

        Task<T> Update(T entity);

        Task<Result> Delete(T entity);

        Task<Result> Delete(int id);

        Task<bool> Exists(int id);

        Task<bool> Exists(Expression<Func<T, bool>> condition);

        Task<Result> EnsureExists(int id);

        Task<T> GetByIdOrNull(int id);

        Task<Result<T>> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> FirstOrNull(Expression<Func<T, bool>> condition);

        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> condition);
    }
}
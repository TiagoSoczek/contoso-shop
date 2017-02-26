using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contoso.Shop.Infra.Shared.Data;
using Contoso.Shop.Model.Shared;
using Contoso.Shop.Model.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Shop.Infra.Shared.Repositories
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {
        private readonly ShopDataContext context;

        public EntityFrameworkRepository(ShopDataContext context)
        {
            this.context = context;
        }

        public async Task<T> Insert(T entity)
        {
            context.Add(entity);

            await SaveChangesAsync();

            return entity;
        }

        public Task Insert(IEnumerable<T> entities)
        {
            context.AddRange(entities);

            return SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            context.Update(entity);

            await SaveChangesAsync();

            return entity;
        }

        public Task Update(IEnumerable<T> entities)
        {
            context.UpdateRange(entities);

            return SaveChangesAsync();
        }

        public async Task<Result> Delete(T entity)
        {
            context.Remove(entity);

            var affectedRows = await SaveChangesAsync();

            return affectedRows > 0 ? Result.Ok() : CommonResults.EntityNotFoundToRemove<T>(entity.Id);
        }

        public async Task<Result> Delete(int id)
        {
            // TODO: Avoid getting from database or add constraint new()
            var entity = await GetByIdOrNull(id);

            if (entity == null)
            {
                return CommonResults.EntityNotFoundToRemove<T>(id);
            }

            return await Delete(entity);
        }

        public Task<bool> Exists(Expression<Func<T, bool>> condition)
        {
            return context.Set<T>().AnyAsync(condition);
        }

        public Task<bool> Exists(int id)
        {
            return Exists(x => x.Id == id);
        }

        public async Task<Result> EnsureExists(int id)
        {
            var exists = await Exists(id);

            return exists ? Result.Ok() : CommonResults.NotFound<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdOrNull(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<Result<T>> GetById(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return CommonResults.NotFound<T>(id);
            }

            return Result.Ok(entity);
        }

        public Task<T> FirstOrNull(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        private Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
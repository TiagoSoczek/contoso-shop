using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Contoso.Shop.Model.Shared;
using Contoso.Shop.Model.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace Contoso.Shop.Infra.Shared.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : Entity
    {
        private readonly ILogger logger;
        private readonly ConcurrentDictionary<int, T> data = new ConcurrentDictionary<int, T>();
        private int id;

        public InMemoryRepository(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger($"InMemoryRepository-{typeof(T).Name}");
        }

        public Task<Result> Delete(T entity)
        {
            return Delete(entity.Id);
        }

        public async Task<Result> Delete(int entityId)
        {
            T entity;

            if (data.TryRemove(entityId, out entity))
            {
                logger.LogTrace($"#{entityId} removed");

                return Result.Ok();
            }

            logger.LogTrace($"#{entityId} not found");

            return CommonResults.EntityNotFoundToRemove<T>(entityId);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            logger.LogTrace("Returning All");

            return data.Values;
        }

        public async Task<T> Update(T entity)
        {
            if (data.TryUpdate(entity.Id, entity, entity))
            {
                logger.LogTrace($"#{entity.Id} updated");

                return entity;
            }

            throw new InvalidOperationException($"{typeof(T).Name} not exists with id {entity.Id}");
        }

        public Task Insert(IEnumerable<T> entities)
        {
            return Task.WhenAll(entities.Select(Insert));
        }

        public async Task<T> Insert(T entity)
        {
            entity.SetId(Interlocked.Increment(ref id));

            if (data.TryAdd(entity.Id, entity))
            {
                logger.LogTrace($"#{entity.Id} inserted");

                return entity;
            }

            logger.LogTrace($"#{entity.Id} can't be added");

            // NOTE: no need to throw
            return entity;
        }

        public Task Update(IEnumerable<T> entities)
        {
            return Task.WhenAll(entities.Select(Update));
        }

        public Task<bool> Exists(int id)
        {
            return Exists(x => id.Equals(x.Id));
        }

        public async Task<Result> EnsureExists(int id)
        {
            var exists = await Exists(id);

            return exists ? Result.Ok() : CommonResults.NotFound<T>(id);
        }

        public async Task<T> GetByIdOrNull(int id)
        {
            T entity;

            if (data.TryGetValue(id, out entity))
            {
                logger.LogTrace($"#{id} returned");

                return entity;
            }

            logger.LogTrace($"#{id} not found");

            return null;
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> condition)
        {
            return data.Values.Any(condition.Compile());
        }

        public async Task<T> FirstOrNull(Expression<Func<T, bool>> condition)
        {
            return data.Values.FirstOrDefault(condition.Compile());
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> condition)
        {
            return data.Values.Where(condition.Compile());
        }

        public async Task<Result<T>> GetById(int id)
        {
            var entity = await GetByIdOrNull(id);

            return entity == null ? CommonResults.NotFound<T>(id) : Result.Ok(entity);
        }
    }
}
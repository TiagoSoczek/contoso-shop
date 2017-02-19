using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        public Task Delete(T entity)
        {
            if (data.TryRemove(entity.Id, out entity))
            {
                logger.LogTrace($"#{entity.Id} removed");

                return Task.CompletedTask;
            }

            logger.LogTrace($"#{entity.Id} not found");

            throw new InvalidOperationException($"{typeof(T).Name} not exists with id {entity.Id}");
        }

        public async Task<bool> Exists(Func<T, bool> predicate)
        {
            return data.Values.Any(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            logger.LogTrace("Returning All");

            return data.Values;
        }

        public async Task<T> GetById(int id)
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

        public async Task<T> Update(T entity)
        {
            if (data.TryUpdate(entity.Id, entity, entity))
            {
                logger.LogTrace($"#{entity.Id} updated");

                return entity;
            }

            throw new InvalidOperationException($"{typeof(T).Name} not exists with id {entity.Id}");
        }
    }
}
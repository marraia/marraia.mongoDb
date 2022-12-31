using Marraia.MongoDb.Core;
using Marraia.MongoDb.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marraia.MongoDb.Repositories
{
    public abstract class MongoDbRepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
                   where TEntity : Entity<TKey>
                   where TKey : struct
    {
        private readonly IMongoContext _context;

        public virtual IMongoCollection<TEntity> Collection
        {
            get
            {
                return _context
                        .Database
                        .GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        public MongoDbRepositoryBase(IMongoContext context)
        {
            _context = context;
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await Collection
                    .InsertOneAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            await Collection
                    .ReplaceOneAsync(filter, entity);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            await Collection
                    .DeleteOneAsync(filter);
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            var entity = await Collection
                                .FindAsync(filter)
                                .ConfigureAwait(false);

            return entity.FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Collection
                            .AsQueryable()
                            .ToListAsync();
        }
    }
}

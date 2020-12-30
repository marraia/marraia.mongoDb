using Marraia.MongoDb.Core;
using Marraia.MongoDb.Repositories.Interfaces;
using MongoDB.Driver;

namespace Marraia.MongoDb.Repositories
{
    public class MongoDbRepositoryStandard<TEntity, TKey> 
                   where TEntity : Entity<TKey>
                   where TKey : struct
    {
        private readonly IMongoContext _context;

        protected IMongoCollection<TEntity> Collection
        {
            get
            {
                return _context
                        .Database
                        .GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        public MongoDbRepositoryStandard(IMongoContext context)
        {
            _context = context;
        }
    }
}

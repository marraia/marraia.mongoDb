using Marraia.MongoDb.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marraia.MongoDb.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity, TKey>
                where TEntity : Entity<TKey>
                where TKey : struct
    {
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey entity);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}

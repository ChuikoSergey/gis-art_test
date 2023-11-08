using Trips.Core.Entities.Base;

namespace Trips.Core.Data.Repository;

public interface IRepository<TEntity> where TEntity: class, IEntity
{
    IQueryable<TEntity> Data { get; }
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task RemoveAsync(int id);
    Task BulkCreateAsync(IEnumerable<TEntity> entities);
    Task BulkUpdateAsync(IEnumerable<TEntity> entities);
    Task BulkRemoveAsync(IEnumerable<TEntity> entities);
    Task<int> SaveChangesAsync();
}

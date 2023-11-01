using Trips.Core.Entities.Base;

namespace Trips.Core.DataService.BaseCrudService;

public interface IBaseDataService<TEntity> where TEntity: class, IEntity
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<Guid> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(Guid id);
    Task BulkCreateAsync(IEnumerable<TEntity> entities);
    Task BulkUpdateAsync(IEnumerable<TEntity> entities);
    Task BulkRemoveAsync(IEnumerable<Guid> ids);
}

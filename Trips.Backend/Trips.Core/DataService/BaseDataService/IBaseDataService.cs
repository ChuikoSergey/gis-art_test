using Trips.Core.Entities.Base;

namespace Trips.Core.DataService.BaseCrudService;

public interface IBaseDataService<TEntity> where TEntity: class, IEntity
{
    Task<TEntity> GetByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
    Task<int> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(int id);
    Task BulkCreateAsync(IEnumerable<TEntity> entities);
    Task BulkUpdateAsync(IEnumerable<TEntity> entities);
    Task BulkRemoveAsync(IEnumerable<int> ids);
}

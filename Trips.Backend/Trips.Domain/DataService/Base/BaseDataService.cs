using Trips.Core.Data.Repository;
using Trips.Core.DataService.BaseCrudService;
using Trips.Core.Entities.Base;

namespace Trips.Domain.DataService.Base;

public abstract class BaseDataService<TEntity> : IBaseDataService<TEntity> where TEntity : class, IEntity
{
    protected readonly IRepository<TEntity> _repository;

    public BaseDataService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task BulkCreateAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && entities.Any())
        {
            await _repository.BulkCreateAsync(entities);
            await _repository.SaveChangesAsync();
        }
    }

    public async Task BulkRemoveAsync(IEnumerable<Guid> ids)
    {
        if (ids != null && ids.Any())
        {
            var entities = _repository.Data.Where(e => ids.Contains(e.Id)).ToList();
            if (entities.Any())
            {
                await _repository.BulkRemoveAsync(entities);
                await _repository.SaveChangesAsync();
            }
        }
    }

    public async Task BulkUpdateAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && entities.Any())
        {
            await _repository.BulkUpdateAsync(entities);
            await _repository.SaveChangesAsync();
        }
    }

    public async Task<Guid> CreateAsync(TEntity entity)
    {
        if (entity != null)
        {
            await _repository.CreateAsync(entity);
            await _repository.SaveChangesAsync();
            return entity.Id;
        }
        return Guid.Empty;
    }

    public Task<bool> ExistsByIdAsync(Guid id)
    {
        return Task.FromResult(_repository.Data.Any(e => e.Id == id));
    }

    public Task<TEntity> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_repository.Data.FirstOrDefault(e => e.Id == id));
    }

    public async Task RemoveAsync(Guid id)
    {
        await _repository.RemoveAsync(id);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        if (entity != null)
        {
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }
    }
}

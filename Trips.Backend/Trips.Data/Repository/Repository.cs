using Microsoft.EntityFrameworkCore;
using Trips.Core.Data.Repository;
using Trips.Core.Entities.Base;
using Trips.Data.Context;

namespace Trips.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly SysContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(SysContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> Data => _dbSet.AsQueryable();

    public Task BulkCreateAsync(IEnumerable<TEntity> entities)
    {
        var tasks = entities.Select(e => CreateAsync(e));
        return Task.WhenAll(tasks);
    }

    public Task BulkRemoveAsync(IEnumerable<TEntity> entities)
    {
        if (entities.All(e => _dbSet.Any(entity => e.Id == entity.Id)))
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
            return Task.CompletedTask;
        }
        else 
        {
            throw new Exception("Bulk remove exception. Some of given record have wrong id.");
        }
    }

    public Task BulkUpdateAsync(IEnumerable<TEntity> entities)
    {
        if (entities.All(e => _dbSet.Any(entity => e.Id == entity.Id)))
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            return Task.CompletedTask;
        }
        else 
        {
            throw new Exception("Bulk update exception. Some of given record have wrong id.");
        }
    }

    public Task CreateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        return Task.CompletedTask;
    }

    public Task RemoveAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        return Task.CompletedTask;
    }

    public async Task RemoveAsync(int id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
        {
            throw new Exception("Delete exception. No record found with given entity id.");
        }
        await RemoveAsync(entity);
    }

    public Task<int> SaveChangesAsync() =>
        _context.SaveChangesAsync();

    public async Task UpdateAsync(TEntity entity)
    {
        if(await _dbSet.AnyAsync(e => e.Id == entity.Id))
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        else
        {
            throw new Exception("Update exception. No record found with given entity id.");
        }
    }

    
}

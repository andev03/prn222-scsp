using SCSP.DataAccess.Repositories.Interfaces;
using SCSP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SCSP.DataAccess.Repositories.Implements;

public class CrudRepository<T> : ICrudRepository<T> where T : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<T> DbSet;

    public CrudRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsQueryable();
        
        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.Where(predicate);
        
        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.CreatedAt = DateTime.UtcNow;
            baseEntity.UpdatedAt = DateTime.UtcNow;
            baseEntity.IsDeleted = false;
            baseEntity.Disabled = false;
        }

        if (entity is BaseEntityWithGuid guidEntity && guidEntity.Guid == Guid.Empty)
        {
            guidEntity.Guid = Guid.NewGuid();
        }

        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedAt = DateTime.UtcNow;
                baseEntity.UpdatedAt = DateTime.UtcNow;
                baseEntity.IsDeleted = false;
                baseEntity.Disabled = false;
            }
        }

        await DbSet.AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.UpdatedAt = DateTime.UtcNow;
        }

        DbSet.Update(entity);
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.UpdatedAt = DateTime.UtcNow;
            }
        }

        DbSet.UpdateRange(entities);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.IsDeleted = true;
            baseEntity.DeletedAt = DateTime.UtcNow;
            baseEntity.UpdatedAt = DateTime.UtcNow;
            DbSet.Update(entity);
        }
        else
        {
            DbSet.Remove(entity);
        }

        await Context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.DeletedAt = DateTime.UtcNow;
                baseEntity.UpdatedAt = DateTime.UtcNow;
            }
        }

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            DbSet.UpdateRange(entities);
        }
        else
        {
            DbSet.RemoveRange(entities);
        }

        await Context.SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task HardDeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.IgnoreQueryFilters().ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetDeletedEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var query = DbSet.IgnoreQueryFilters().AsQueryable();

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => (e as BaseEntity) != null &&
                                     ((e as BaseEntity)!).IsDeleted.HasValue &&
                                     ((e as BaseEntity)!).IsDeleted!.Value);
        }
        else
        {
            return new List<T>();
        }

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task RestoreAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.IsDeleted = false;
            baseEntity.DeletedAt = null;
            baseEntity.UpdatedAt = DateTime.UtcNow;
            DbSet.Update(entity);
        }
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsQueryable();
        
        return await query.CountAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.Where(predicate);
        
        return await query.CountAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.Where(predicate);
        
        return await query.AnyAsync(cancellationToken);
    }

    public virtual async Task EnableAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.Disabled = false;
            baseEntity.UpdatedAt = DateTime.UtcNow;
            DbSet.Update(entity);
        }
    }

    public virtual async Task DisableAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.Disabled = true;
            baseEntity.UpdatedAt = DateTime.UtcNow;
            DbSet.Update(entity);
        }
    }
}
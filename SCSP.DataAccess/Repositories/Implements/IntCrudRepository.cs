using BusinessObjects.Models.Base;
using SCSP.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SCSP.DataAccess.Repositories.Implements;

public class IntCrudRepository<T> : CrudRepository<T>, IIntCrudRepository<T> where T : BaseEntityWithInt
{
    public IntCrudRepository(DbContext context) : base(context)
    {
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity != null && entity.IsDeleted == true)
            return null;

        return entity;
    }

    public virtual async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entity != null)
        {
            await DeleteAsync(entity, cancellationToken);
        }
    }

    public virtual async Task HardDeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entity != null)
        {
            await HardDeleteAsync(entity, cancellationToken);
        }
    }

    public virtual async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        return entity != null;
    }
}
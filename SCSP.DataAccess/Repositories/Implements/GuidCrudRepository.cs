using BusinessObjects.Models.Base;
using SCSP.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SCSP.DataAccess.Repositories.Implements;

public class GuidCrudRepository<T> : CrudRepository<T>, IGuidCrudRepository<T> where T : BaseEntityWithGuid
{
    public GuidCrudRepository(DbContext context) : base(context)
    {
    }

    public virtual async Task<T?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Guid == guid, cancellationToken);

        if (entity != null && entity.IsDeleted == true)
            return null;

        return entity;
    }

    public virtual async Task DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Guid == guid, cancellationToken);
        if (entity != null)
        {
            await DeleteAsync(entity, cancellationToken);
        }
    }

    public virtual async Task HardDeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Guid == guid, cancellationToken);
        if (entity != null)
        {
            await HardDeleteAsync(entity, cancellationToken);
        }
    }

    public virtual async Task<bool> ExistsByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var entity = await GetByGuidAsync(guid, cancellationToken);
        return entity != null;
    }
}
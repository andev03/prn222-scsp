using SCSP.DataAccess.Models;

namespace SCSP.DataAccess.Repositories.Interfaces;

public interface IGuidCrudRepository<T> : ICrudRepository<T> where T : BaseEntityWithGuid
{
    Task<T?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default);
    Task DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default);
    Task HardDeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default);
    Task<bool> ExistsByGuidAsync(Guid guid, CancellationToken cancellationToken = default);
}
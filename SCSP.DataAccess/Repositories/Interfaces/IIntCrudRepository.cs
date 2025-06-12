using BusinessObjects.Models.Base;

namespace SCSP.DataAccess.Repositories.Interfaces;

public interface IIntCrudRepository<T> : ICrudRepository<T> where T : BaseEntityWithInt
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    Task HardDeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken = default);
}
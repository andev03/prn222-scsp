using BusinessObjects.Models.Base;
using Microsoft.EntityFrameworkCore.Storage;
using SCSP.DataAccess.Repositories.Interfaces;

namespace SCSP.DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICrudRepository<T> GetRepository<T>() where T : BaseEntity;
    IGuidCrudRepository<T> GetGuidRepository<T>() where T : BaseEntityWithGuid;
    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}
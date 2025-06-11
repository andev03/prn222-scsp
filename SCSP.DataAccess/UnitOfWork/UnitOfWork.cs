using BusinessObjects.Models.Base;
using Microsoft.EntityFrameworkCore.Storage;
using SCSP.DataAccess.Models;
using SCSP.DataAccess.Repositories.Implements;
using SCSP.DataAccess.Repositories.Interfaces;

namespace SCSP.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ScspContext _context;
    private readonly Dictionary<Type, object> _repositories;
    private readonly Dictionary<Type, object> _guidRepositories;
    private bool _disposed = false;

    public UnitOfWork(ScspContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new Dictionary<Type, object>();
        _guidRepositories = new Dictionary<Type, object>();
    }

    public ICrudRepository<T> GetRepository<T>() where T : BaseEntity
    {
        var type = typeof(T);

        if (_repositories.ContainsKey(type))
        {
            return (ICrudRepository<T>)_repositories[type];
        }

        var repository = new CrudRepository<T>(_context);
        _repositories.Add(type, repository);
        return repository;
    }

    public IGuidCrudRepository<T> GetGuidRepository<T>() where T : BaseEntityWithGuid
    {
        var type = typeof(T);

        if (_guidRepositories.ContainsKey(type))
        {
            return (IGuidCrudRepository<T>)_guidRepositories[type];
        }

        var repository = new GuidCrudRepository<T>(_context);
        _guidRepositories.Add(type, repository);
        return repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context?.Dispose();
            _repositories.Clear();
            _guidRepositories.Clear();
        }
        _disposed = true;
    }
}
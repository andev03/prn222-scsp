using Microsoft.Extensions.DependencyInjection;
using SCSP.DataAccess.Repositories.Interfaces;
using SCSP.DataAccess.Repositories.Implements;
using SCSP.DataAccess.UnitOfWork;

namespace SCSP.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
        services.AddScoped(typeof(IGuidCrudRepository<>), typeof(GuidCrudRepository<>));
        return services;
    }
    
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddUnitOfWork();
        return services;
    }
} 
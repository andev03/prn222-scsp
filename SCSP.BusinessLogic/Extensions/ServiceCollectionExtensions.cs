using Microsoft.Extensions.DependencyInjection;

namespace SCSP.BusinessLogic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(assembly);
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddServices();
        services.AddAutoMapper();
        return services;
    }
}
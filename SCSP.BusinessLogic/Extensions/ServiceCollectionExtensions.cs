using Microsoft.Extensions.DependencyInjection;
using SCSP.BusinessLogic.Services.Implements;
using SCSP.BusinessLogic.Services.Interfaces;

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
        services.AddScoped<IFirebaseService, FirebaseService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        
        
        return services;
    }
    
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddServices();
        services.AddAutoMapper();
        return services;
    }
} 
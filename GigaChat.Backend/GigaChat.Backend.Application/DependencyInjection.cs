using GigaChat.Backend.Application.Errors;
using Microsoft.Extensions.DependencyInjection;

namespace GigaChat.Backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddExceptionHandler();
        
        return services;
    }
    
    private static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>(); // to register the GlobalExceptionHandler as the default exception handler
        services.AddProblemDetails();

        return services;
    }
}
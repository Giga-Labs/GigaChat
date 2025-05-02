using FluentValidation;
using FluentValidation.AspNetCore;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Settings;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace GigaChat.Backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddExceptionHandler();

        services.AddFluentValidationConfig();

        services.AddMediatRConfiguration();

        services.AddMapsterConfigurations();

        services.AddSettings();
        
        return services;
    }
    
    private static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>(); // to register the GlobalExceptionHandler as the default exception handler
        services.AddProblemDetails();
        
        return services;
    }
    
    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly)
            .AddFluentValidationAutoValidation();

        return services;
    }
    
    private static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));

        return services;
    }
    
    private static IServiceCollection AddMapsterConfigurations(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(typeof(ApplicationAssemblyMarker).Assembly);

        return services;
    }
    
    private static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddOptions<RefreshTokenSettings>()
            .BindConfiguration(RefreshTokenSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<OtpRateSettings>()
            .BindConfiguration(OtpRateSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
    
    
}
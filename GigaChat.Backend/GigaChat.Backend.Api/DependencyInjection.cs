using GigaChat.Backend.Api.Services.Hubs;
using GigaChat.Backend.Application;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Infrastructure;

namespace GigaChat.Backend.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        
        services.AddOpenApi();

        services.AddInfrastructureLayerServices(config);

        services.AddApplicationLayerServices();

        services.AddCors();

        services.AddHubServices();

        services.AddSignalR();
        
        return services;
    }
    
    private static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.WithOrigins("http://localhost:5173", "https://gigachat.vercel.app", "https://gigachat.tech") 
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
    
    private static IServiceCollection AddHubServices(this IServiceCollection services)
    {
        services.AddScoped<IConversationHubNotifier, ConversationHubNotifier>();

        services.AddScoped<IMessageHubNotifier, MessageHubNotifier>();
        
        services.AddScoped<IConversationBroadcaster, ConversationBroadcaster>();
        
        services.AddScoped<IMessageBroadcaster, MessageBroadcaster>();

        return services;
    }
}
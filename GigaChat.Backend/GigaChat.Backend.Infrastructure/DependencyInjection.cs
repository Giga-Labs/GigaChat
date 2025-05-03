using System.Text;
using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Services.Email;
using GigaChat.Backend.Application.Services.Otp;
using GigaChat.Backend.Infrastructure.Auth;
using GigaChat.Backend.Infrastructure.Persistence.Identity;
using GigaChat.Backend.Infrastructure.Persistence.Identity.Entities;
using GigaChat.Backend.Infrastructure.Repositories.Identity;
using GigaChat.Backend.Infrastructure.Services.Email;
using GigaChat.Backend.Infrastructure.Services.Otp;
using GigaChat.Backend.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace GigaChat.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddApplicationUserDbContext(config);

        services.AddIdentityServices();

        services.AddAuthenticationServices(config);

        services.AddSettings();

        services.AddEmailService();

        services.AddOtpVerificationRepository();

        services.AddOtpServices();
        
        return services;
    }
    
    private static IServiceCollection AddApplicationUserDbContext(this IServiceCollection services,
        IConfiguration config)
    {
        var identityDbConnectionString = config.GetConnectionString("IdentityDbConnectionString") ??
                                         throw new InvalidOperationException(
                                             "Can't find IdentityDb connection string.");
        services.AddDbContext<ApplicationUserDbContext>(options => options.UseSqlServer(identityDbConnectionString));

        return services;
    }
    
    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password Configuration
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout Configuration
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // Email Configuration
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationUserDbContext>()  // Store identity data in our DB
            .AddDefaultTokenProviders(); // Enables email confirmation, password reset, etc.

        // Register User Repository
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = config.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                };
            });

        return services;
    }
    
    public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) =>
        {
            var environment = context.HostingEnvironment.EnvironmentName;

            configuration.ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId();

            if (environment == "Development")
            {
                configuration
                    .MinimumLevel.Information()
                    .WriteTo.Console(outputTemplate:
                        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} " +
                        "[Machine: {MachineName}] [ThreadId: {ThreadId}] [Process: {ProcessId}]{NewLine}{Exception}")
                    .WriteTo.File("../GigaChat/logs/dev-log-.log", rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} " +
                                        "[Machine: {MachineName}] [ThreadId: {ThreadId}] [Process: {ProcessId}]{NewLine}{Exception}");
            }
            else // Production
            {
                configuration
                    .MinimumLevel.Warning()
                    .WriteTo.File("../GigaChat/logs/log-.log", rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} " +
                                        "[Machine: {MachineName}] [ThreadId: {ThreadId}] [Process: {ProcessId}]{NewLine}{Exception}");
            }
        });

        return hostBuilder;
    }
    
    private static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddOptions<AppSettings>()
            .BindConfiguration(AppSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<MailSettings>()
            .BindConfiguration(MailSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<AdminSettings>()
            .BindConfiguration(AdminSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<OtpSettings>()
            .BindConfiguration(OtpSettings.SectionName)
            .ValidateDataAnnotations()
            .Validate(otp => otp.Verification.MaxAttempts > 0 && otp.PasswordReset.MaxAttempts > 0 && otp.TwoFactor.MaxAttempts > 0, "OTP configuration must be valid.")
            .ValidateOnStart();
        
        return services;
    }
    
    private static IServiceCollection AddEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    private static IServiceCollection AddOtpVerificationRepository(this IServiceCollection services)
    {
        services.AddScoped<IOtpVerificationRepository, OtpVerificationRepository>();

        return services;
    }
    
    private static IServiceCollection AddOtpServices(this IServiceCollection services)
    {
        services.AddScoped<IOtpHashingService, OtpHashingService>();
        
        services.AddScoped<IOtpGenerator, OtpGenerator>();

        services.AddScoped<IOtpProvider, OtpProvider>();

        return services;
    }

}
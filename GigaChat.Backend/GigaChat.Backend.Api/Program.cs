using GigaChat.Backend.Api;
using GigaChat.Backend.Api.Hubs;
using GigaChat.Backend.Infrastructure;
using GigaChat.Backend.Infrastructure.Persistence.Identity;
using GigaChat.Backend.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

builder.Host.AddSerilog();

var app = builder.Build();

// seed the database(s)
using (var scope = app.Services.CreateScope())
{
    // seed identity
    var identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationUserDbContext>();
    identityDbContext.Database.Migrate();
    await IdentitySeeder.SeedAdminUserAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("ApiDocumentation:Enabled")) 
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Agrivision API");
        options.RoutePrefix = "swagger";
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ConversationHub>("/hubs/conversations").RequireCors("AllowSpecificOrigin");

app.MapHub<MessageHub>("/hubs/messages").RequireCors("AllowSpecificOrigin");

app.MapControllers();

app.UseExceptionHandler();

app.Run();
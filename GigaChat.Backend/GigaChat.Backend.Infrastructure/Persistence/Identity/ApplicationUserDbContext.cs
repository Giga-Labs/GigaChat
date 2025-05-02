using GigaChat.Backend.Domain.Entities.Identity;
using GigaChat.Backend.Infrastructure.Persistence.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Persistence.Identity;

public class ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<OtpVerification> OtpVerifications { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(InfrastructureAssemblyMarker).Assembly,
            x => x.Namespace != null && x.Namespace.Contains("Identity"));
        
        base.OnModelCreating(modelBuilder);
    }
}
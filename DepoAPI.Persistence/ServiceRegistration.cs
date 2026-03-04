using DepoAPI.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DepoAPI.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DepoAPIDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
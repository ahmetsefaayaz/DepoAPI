using DepoAPI.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DepoAPI.Persistence;

public class DepoAPIDbContextFactory : IDesignTimeDbContextFactory<DepoAPIDbContext>
{
    public DepoAPIDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DepoAPIDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DepoDb;Username=postgres;Password=*Eag123*");

        return new DepoAPIDbContext(optionsBuilder.Options);
    }
}

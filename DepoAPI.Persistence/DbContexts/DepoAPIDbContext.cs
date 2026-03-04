using DepoAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DepoAPI.Persistence.DbContexts;

public class DepoAPIDbContext: IdentityDbContext<User, Role, Guid>
{
    public DepoAPIDbContext(DbContextOptions<DepoAPIDbContext> options) : base(options) {}
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Depo> Depos { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}
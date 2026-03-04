using DepoAPI.Application.Interfaces.IProduct;
using DepoAPI.Domain.Entities;
using DepoAPI.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DepoAPI.Persistence.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly DepoAPIDbContext _context;

    public ProductRepository(DepoAPIDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
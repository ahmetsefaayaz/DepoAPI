using DepoAPI.Application.Interfaces.IDepo;
using DepoAPI.Domain.Entities;
using DepoAPI.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DepoAPI.Persistence.Repositories;

public class DepoRepository: IDepoRepository
{
    private readonly DepoAPIDbContext _context;

    public DepoRepository(DepoAPIDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<Depo> GetAsync(Guid id)
    {
        return await _context.Depos
            .Include(x => x.Customer)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Depo>> GetAllAsync()
    {
        return await _context.Depos.
            Include(x => x.Customer) 
            .Include(x => x.Products)
            .ToListAsync();
    }

    public async Task AddAsync(Depo depo)
    {
        await _context.Depos.AddAsync(depo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Depo depo)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Depo depo)
    {
        _context.Depos.Remove(depo);
        await _context.SaveChangesAsync();
    }
}
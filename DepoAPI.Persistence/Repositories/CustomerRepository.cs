using DepoAPI.Application.Interfaces.ICustomer;
using DepoAPI.Domain.Entities;
using DepoAPI.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DepoAPI.Persistence.Repositories;

public class CustomerRepository: ICustomerRepository
{
    private readonly DepoAPIDbContext _context;

    public CustomerRepository(DepoAPIDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<Customer> GetByIdAsync(Guid id)
    {
        return await _context.Customers
            .Include(customer => customer.Depos)
            .FirstOrDefaultAsync(customer => customer.Id == id);
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .Include(customer => customer.Depos)
            .ToListAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}
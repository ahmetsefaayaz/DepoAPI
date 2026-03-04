using DepoAPI.Application.Dtos.ProductDTO;
using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Interfaces.IProduct;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(Guid id);
    Task <List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}
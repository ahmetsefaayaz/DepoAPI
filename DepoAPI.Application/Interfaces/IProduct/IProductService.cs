using DepoAPI.Application.Dtos.ProductDTO;
using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Interfaces.IProduct;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task AddProductAsync(Guid depoId, AddProductDto dto);
    Task UpdateProductAsync(Guid id, UpdateProductDto dto);
    Task DeleteProductAsync(Guid id);
}
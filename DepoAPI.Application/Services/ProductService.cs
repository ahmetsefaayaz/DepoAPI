using DepoAPI.Application.Dtos.ProductDTO;
using DepoAPI.Application.Interfaces.IDepo;
using DepoAPI.Application.Interfaces.IProduct;
using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Services;

public class ProductService:  IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IDepoRepository _depoRepository;

    public ProductService(IProductRepository productRepository,  IDepoRepository depoRepository)
    {
        _productRepository = productRepository;
        _depoRepository = depoRepository;
    }
    
    
    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products;
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product;
    }

    public async Task AddProductAsync(Guid depoId, AddProductDto dto)
    {
        var depo = await _depoRepository.GetAsync(depoId);
        if (depo == null)
        {
            throw new Exception("Depo bulunamadi");
        }
        Product product = new Product
        {
            DepoId = depoId,
            ProductName = dto.ProductName,
            Description = dto.Description,
            Stock = dto.Stock
        };
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Guid id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception("Urun bulunamadi");
        }
        product.ProductName = dto.ProductName;
        product.Description = dto.Description;
        product.Stock = dto.Stock;
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception("Urun bulunamadi");
        }
        await _productRepository.DeleteAsync(product);
    }
}
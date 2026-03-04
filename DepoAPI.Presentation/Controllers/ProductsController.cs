using DepoAPI.Application.Dtos.ProductDTO;
using DepoAPI.Application.Interfaces.IProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepoAPI.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }
    
    [Authorize (Policy = "CanModeratePolicy")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    [Authorize (Policy = "CanModeratePolicy")]
    [HttpPost]
    public async Task<IActionResult> AddProduct(Guid depoId, AddProductDto dto)
    {
        await _productService.AddProductAsync(depoId, dto);
        return Ok();
    }

    [Authorize (Policy = "CanModeratePolicy")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProductAsync(id);
        return Ok();
    }

    [Authorize (Policy = "CanModeratePolicy")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto dto)
    {
        await _productService.UpdateProductAsync(id, dto);
        return Ok();
    }
    
    
}
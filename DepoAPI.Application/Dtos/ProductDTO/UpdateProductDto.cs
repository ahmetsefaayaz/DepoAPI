namespace DepoAPI.Application.Dtos.ProductDTO;

public class UpdateProductDto
{
    
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
}
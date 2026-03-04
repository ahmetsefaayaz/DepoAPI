namespace DepoAPI.Application.Dtos.ProductDTO;

public class AddProductDto
{
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
}
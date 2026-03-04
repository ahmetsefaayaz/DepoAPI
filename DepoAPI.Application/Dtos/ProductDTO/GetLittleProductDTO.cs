namespace DepoAPI.Application.Dtos.ProductDTO;

public class GetLittleProductDTO
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public int Stock { get; set; }
}
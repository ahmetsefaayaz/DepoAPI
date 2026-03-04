namespace DepoAPI.Domain.Entities;

public class Product
{
    public Guid DepoId { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace DepoAPI.Domain.Entities;

public class Depo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CreatorId { get; set; }
    [Required]
    public Customer Customer { get; set; }
    public ICollection<Product>?  Products { get; set; }
}
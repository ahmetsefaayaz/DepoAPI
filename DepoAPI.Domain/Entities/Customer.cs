namespace DepoAPI.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public Guid OwnerId { get; set; }
    public ICollection<Depo>? Depos { get; set; }
    
}
using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Dtos.DepoDTO;

public class UpdateDepoDTO
{
    public ICollection<Product>?  Products { get; set; }
}
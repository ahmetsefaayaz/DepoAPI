using DepoAPI.Application.Dtos.CustomerDto;
using DepoAPI.Application.Dtos.ProductDTO;
namespace DepoAPI.Application.Dtos.DepoDTO;

public class GetDepoDTO
{
    public Guid Id { get; set; }
    public LittleCustomerDto LittleCustomer { get; set; }
    public ICollection<GetLittleProductDTO>? LittleProducts { get; set; }
}
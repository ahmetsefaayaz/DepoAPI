using DepoAPI.Application.Dtos.DepoDTO;
using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Dtos.CustomerDto;

public class GetCustomerDTO
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public ICollection<GetLittleDepoDTO>? LittleDepos { get; set; }
}
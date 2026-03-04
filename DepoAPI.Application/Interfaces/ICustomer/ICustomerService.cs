using DepoAPI.Application.Dtos.CustomerDto;
using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Interfaces.ICustomer;

public interface ICustomerService
{
    Task<GetCustomerDTO> GetCustomerByIdAsync(Guid id);
    Task<List<GetCustomerDTO>> GetAllCustomersAsync();
    Task AddCustomerAsync(AddCustomerDTO dto);
    Task DeleteCustomerAsync(Guid id);
    Task UpdateCustomerAsync(Guid id, UpdateCustomerDTO dto);
}
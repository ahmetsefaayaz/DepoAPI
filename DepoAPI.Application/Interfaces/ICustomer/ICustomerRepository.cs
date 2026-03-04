using DepoAPI.Domain.Entities;
namespace DepoAPI.Application.Interfaces.ICustomer;

public interface ICustomerRepository
{
    Task <Customer>GetByIdAsync(Guid id);
    Task <List<Customer>>GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
}
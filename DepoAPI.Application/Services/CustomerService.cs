using System.Security.Claims;
using DepoAPI.Application.Dtos.CustomerDto;
using DepoAPI.Application.Dtos.DepoDTO;
using DepoAPI.Application.Interfaces.ICustomer;
using DepoAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DepoAPI.Application.Services;

public class CustomerService:  ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerService(ICustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)
    {
        _customerRepository = customerRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetCustomerDTO> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            throw new Exception("Musteri bulunamadi");
        }

        return new GetCustomerDTO()
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Address = customer.Address,
            Phone = customer.Phone,
            LittleDepos = customer.Depos?.Select(d => new GetLittleDepoDTO
            {
                Id = d.Id
            }).ToList()
        };
    }

    public async Task<List<GetCustomerDTO>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select(customer => new GetCustomerDTO
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Address = customer.Address,
            Phone = customer.Phone,
            LittleDepos = customer.Depos?.Select(d => new GetLittleDepoDTO
            {
                Id = d.Id
            }).ToList()
         }).ToList();

    }

    public async Task AddCustomerAsync(AddCustomerDTO dto)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) throw new Exception("User not authenticated");
        Customer customer = new Customer()
        {
            FullName = dto.FullName,
            Address = dto.Address,
            Phone = dto.Phone,
            OwnerId = Guid.Parse(userId)
        };
        await _customerRepository.AddAsync(customer);
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            throw new Exception("Musteri bulunamadi");
        }
        await _customerRepository.DeleteAsync(customer);
    }

    public async Task UpdateCustomerAsync(Guid id, UpdateCustomerDTO dto)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            throw new Exception("Musteri bulunamadi");
        }
        customer.FullName = dto.FullName;
        customer.Address = dto.Address;
        customer.Phone = dto.Phone;
        await _customerRepository.UpdateAsync(customer);
    }
}
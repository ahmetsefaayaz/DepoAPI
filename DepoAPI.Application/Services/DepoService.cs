using DepoAPI.Application.Dtos.CustomerDto;
using DepoAPI.Application.Dtos.DepoDTO;
using DepoAPI.Application.Dtos.ProductDTO;
using DepoAPI.Application.Interfaces.ICustomer;
using DepoAPI.Application.Interfaces.IDepo;
using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Services;

public class DepoService: IDepoService
{
    private readonly IDepoRepository _depoRepository;
    private readonly ICustomerRepository _customerRepository;

    public DepoService(IDepoRepository depoRepository, ICustomerRepository customerRepository)
    {
        _depoRepository = depoRepository;
        _customerRepository = customerRepository;
    }
    
    
    
    public async Task<GetDepoDTO> GetDepoByIdAsync(Guid id)
    {
        var depo = await _depoRepository.GetAsync(id);
        if (depo is null)
        {
            throw new Exception("Depo bulunamadi");
        }

        return new GetDepoDTO
        {
            Id = depo.Id,
            LittleProducts = depo.Products?.Select(p => new GetLittleProductDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Stock = p.Stock
            }).ToList(),
            LittleCustomer = new LittleCustomerDto
            {
                Id = depo.Customer.Id,
                FullName = depo.Customer.FullName
            }
        };
    }

    public async Task<List<GetDepoDTO>> GetAllDeposAsync()
    {
        var depos = await _depoRepository.GetAllAsync();
        return depos.Select(depo => new GetDepoDTO
        {
            Id = depo.Id,
            LittleProducts = depo.Products?.Select(p => new GetLittleProductDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Stock = p.Stock
            }).ToList(),
            LittleCustomer = new LittleCustomerDto
            {
                Id = depo.Customer.Id,
                FullName = depo.Customer.FullName
            }
        }).ToList();
    }

    public async Task AddDepoAsync(Guid customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer is null)
        {
            throw new Exception("Musteri bulunamadi");
        }
        Depo depo = new Depo
        {
            Customer =  customer
        };
        await _depoRepository.AddAsync(depo);
    }

    public async Task UpdateDepoAsync(Guid id, UpdateDepoDTO dto)
    {
        var depo = await _depoRepository.GetAsync(id);
        if (depo is null)
        {
            throw new Exception("Depo bulunamadi");
        }

        depo.Products = dto.Products;
        await _depoRepository.UpdateAsync(depo);
    }

    public async Task DeleteDepoAsync(Guid id)
    {
        var depo = await _depoRepository.GetAsync(id);
        if (depo is null)
        {
            throw new Exception("Depo bulunamadi");
        }
        await _depoRepository.DeleteAsync(depo);
    }
}
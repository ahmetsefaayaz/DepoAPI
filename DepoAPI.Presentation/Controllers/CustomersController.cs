using DepoAPI.Application.Dtos.CustomerDto;
using DepoAPI.Application.Interfaces.ICustomer;
using DepoAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepoAPI.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [Authorize (Policy = "OwnsCustomerPolicy")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        return Ok(customer);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody]AddCustomerDTO dto)
    {
        await _customerService.AddCustomerAsync(dto);
        return Ok();
    }

    [Authorize (Policy = "CanModeratePolicy")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerDTO dto)
    {
        await _customerService.UpdateCustomerAsync(id, dto);
        return Ok();
    }

    [Authorize (Policy = "CanModeratePolicy")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        await _customerService.DeleteCustomerAsync(id);
        return Ok();
    }
    
    
}
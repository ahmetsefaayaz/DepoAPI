using DepoAPI.Application.Dtos.DepoDTO;
using DepoAPI.Application.Interfaces.IDepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepoAPI.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DeposController : ControllerBase
{
    private readonly IDepoService _depoService;

    public DeposController(IDepoService depoService)
    {
        _depoService = depoService;
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllDepos()
    {
        var depos = await _depoService.GetAllDeposAsync();
        return Ok(depos);
    }

    [Authorize (Policy = "CanModeratePolicy")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepoById(Guid id)
    {
        var depo = await _depoService.GetDepoByIdAsync(id);
        return Ok(depo);
    }

    [Authorize (Policy = "AtLeast18")]
    [HttpPost]
    public async Task<IActionResult> AddDepo(Guid customerId)
    {
        await _depoService.AddDepoAsync(customerId);
        return Ok();
    }

    [Authorize (Policy = "CanModeratePolicy")]
    [HttpPut]
    public async Task<IActionResult> UpdateDepo(Guid id, UpdateDepoDTO dto)
    {
        var depo = await _depoService.GetDepoByIdAsync(id);
        await _depoService.UpdateDepoAsync(id, dto);
        return Ok();
    }
    [Authorize (Policy = "CanModeratePolicy")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepo(Guid id)
    {
        await _depoService.DeleteDepoAsync(id);
        return Ok();
    }
    
    
}
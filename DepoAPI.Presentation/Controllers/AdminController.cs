using DepoAPI.Application.Interfaces.IUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepoAPI.Presentation.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAuthService _service;

    public AdminController(IAuthService service)
    {
        _service = service;
    }
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole(Guid userId, string role)
    {
        await _service.AssignRoleAsync(userId, role);
        return Ok();
    }
    [HttpPost("assign-claim")]
    public async Task<IActionResult> AssignClaim(Guid id, string type, string value)
    {
        await _service.AssignClaimAsync(id, type, value);
        return Ok();
    }
}
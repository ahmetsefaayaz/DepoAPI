using DepoAPI.Application.Dtos.UserDto;
using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Interfaces.IUser;

public interface IAuthService
{
    Task <string> RegisterAsync(RegisterDto dto);
    Task <string> LoginAsync(LoginDto dto);
    Task AssignRoleAsync(Guid id, string role);
    Task AssignClaimAsync(Guid id, string type, string value);
}
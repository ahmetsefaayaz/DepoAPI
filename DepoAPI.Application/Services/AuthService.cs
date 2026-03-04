using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DepoAPI.Application.Dtos.UserDto;
using DepoAPI.Application.Interfaces.IUser;
using DepoAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DepoAPI.Application.Services;

public class AuthService: IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User>  _passwordHasher = new();
    private readonly UserManager<User> _userManager;

    public AuthService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            throw new Exception($"Mail sistemde mevcut");
        }

        var user = new User
        {
            Email = dto.Email,
            UserName = dto.Username,
            DateOfBirth = dto.DateOfBirth
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
        await _userManager.CreateAsync(user);
        return "kullanici basariyla kayit oldu";
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            throw new Exception($"kullanici sistemde yok");
        }
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("sifre hatali");
        }
        return await GenerateJwtToken(user);
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
        };
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        claims.AddRange(userClaims);
        
        claims.Add(new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd")));
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    

    public async Task AssignRoleAsync(Guid id, string role)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null) throw new Exception("Kullanıcı bulunamadı");
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task AssignClaimAsync(Guid id, string type, string value)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null) throw new Exception("Kullanıcı bulunamadı");
        await _userManager.AddClaimAsync(user, new Claim(type, value));
    }
}
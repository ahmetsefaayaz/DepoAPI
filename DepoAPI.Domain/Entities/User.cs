using Microsoft.AspNetCore.Identity;

namespace DepoAPI.Domain.Entities;

public class User: IdentityUser<Guid>
{
    public DateTime DateOfBirth { get; set; }
}
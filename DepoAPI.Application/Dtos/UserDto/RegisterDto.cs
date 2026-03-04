using System.ComponentModel.DataAnnotations;

namespace DepoAPI.Application.Dtos.UserDto;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Email zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Şifre zorunludur.")]
    [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalı.")] 
    public string Password { get; set; }
    
}
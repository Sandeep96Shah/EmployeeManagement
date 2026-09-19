using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Enums;

namespace EmployeeManagement.Models;

// public class User
// {
//     public Guid Id { get; set; } = Guid.NewGuid();
//     [Required]
//     public string Email { get; set; } = string.Empty;

//     [Required]
//     public string HashPassword { get; set; } = string.Empty;

//     [Required]
//     public Role Role { get; set; }
// }

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public class SignupRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
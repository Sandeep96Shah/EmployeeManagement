using EmployeeManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entity;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public Role Role { get; set; }
}
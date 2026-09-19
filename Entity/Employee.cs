using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entity;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public Guid DepartmentId { get; set; }

    public Department Department { get; set; } = null!;
    public decimal Salary { get; set; } = 0;
}
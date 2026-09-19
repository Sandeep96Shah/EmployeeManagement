using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models;

public class CreateEmployee
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public Guid DepartmentId { get; set; }
    public decimal Salary { get; set; }
}

public class EmplyeeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}

public class EmployeeQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Name { get; set; }
    public Guid? DepartmentId { get; set; }

    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
}

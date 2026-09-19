using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models;

public class CreateDepartment
{
    [Required]
    public string Name { get; set; } = string.Empty;
}

public class DepartmentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<DepartmentEmployeeResponse> Employees { get; set; } = new();
}

public class DepartmentEmployeeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
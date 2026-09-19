using EmployeeManagement.Models;
using EmployeeManagement.Entity;

namespace EmployeeManagement.Services;

public interface IDepartmentService
{
    Task<List<DepartmentResponse>> GetDepartments();
    Task<Department?> GetDepartmentById(Guid id);
    Task<bool> CreateDepartment(CreateDepartment department);
    Task<bool> UpdateDepartment(Department department);
    Task<bool> DeleteDepartment(Guid id);
}

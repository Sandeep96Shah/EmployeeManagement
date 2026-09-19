using EmployeeManagement.Models;
using EmployeeManagement.Entity;

namespace EmployeeManagement.Repositories;

public interface IDepartmentRepository
{
    Task<List<DepartmentResponse>> GetDepartments();
    Task<Department?> GetDepartmentById(Guid id);
    Task<bool> CreateDepartment(CreateDepartment department);
    Task<bool> UpdateDepartment(Department department);
    Task<bool> DeleteDepartment(Guid id);
}
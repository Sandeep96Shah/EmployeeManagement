using EmployeeManagement.Entity;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<DepartmentResponse>> GetDepartments()
    {
        return await _departmentRepository.GetDepartments();
    }

    public async Task<Department?> GetDepartmentById(Guid id)
    {
        return await _departmentRepository.GetDepartmentById(id);
    }

    public async Task<bool> CreateDepartment(CreateDepartment department)
    {
        return await _departmentRepository.CreateDepartment(department);
    }

    public async Task<bool> UpdateDepartment(Department department)
    {
        return await _departmentRepository.UpdateDepartment(department);
    }

    public async Task<bool> DeleteDepartment(Guid id)
    {
        return await _departmentRepository.DeleteDepartment(id);
    }
}

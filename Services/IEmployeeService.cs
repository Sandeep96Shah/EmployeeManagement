using EmployeeManagement.Entity;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services;

public interface IEmployeeService
{
    Task<List<EmplyeeResponse>> GetEmployees(EmployeeQuery query);
    Task<Employee?> GetEmployeeById(Guid id);
    Task<bool> CreateEmployee(CreateEmployee employee);
    Task<bool> UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(Guid id);
}
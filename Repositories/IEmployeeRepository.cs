using EmployeeManagement.Entity;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories;

public interface IEmployeeRepository
{
    Task<List<EmplyeeResponse>> GetEmployees(EmployeeQuery query);
    Task<Employee?> GetEmployeeById(Guid id);
    Task<Employee?> GetEmployeeByEmail(string email);
    Task<bool> CreateEmployee(CreateEmployee employee);
    Task<bool> UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(Guid id);
}
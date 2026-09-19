using EmployeeManagement.Entity;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<EmplyeeResponse>> GetEmployees(EmployeeQuery query)
    {
        return await _employeeRepository.GetEmployees(query);
    }

    public async Task<Employee?> GetEmployeeById(Guid id)
    {
        return await _employeeRepository.GetEmployeeById(id);
    }

    public async Task<bool> CreateEmployee(CreateEmployee employee)
    {
        var existingEmployee = await _employeeRepository.GetEmployeeByEmail(employee.Email);
        if (existingEmployee != null)
        {
            // Handle the case where the employee already exists
            return false;
        }

        await _employeeRepository.CreateEmployee(employee);
        return true;
    }

    public async Task<bool> UpdateEmployee(Employee employee)
    {
        var existingEmployee = await _employeeRepository.GetEmployeeById(employee.Id);
        if (existingEmployee == null)
        {
            // Handle the case where the employee does not exist
            return false;
        }

        await _employeeRepository.UpdateEmployee(employee);
        return true;
    }

    public async Task<bool> DeleteEmployee(Guid id)
    {
        var existingEmployee = await _employeeRepository.GetEmployeeById(id);
        if (existingEmployee == null)
        {
            // Handle the case where the employee does not exist
            return false;
        }

        await _employeeRepository.DeleteEmployee(id);
        return true;
    }
}
using EmployeeManagement.Entity;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeDbContext _context;

    public EmployeeRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmplyeeResponse>> GetEmployees(EmployeeQuery query)
    {
        var employees = _context.Employees
                        .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            employees = employees
                        .Where(emp => emp.Name.Contains(query.Name));
        }

        if (query.DepartmentId.HasValue)
        {
            employees = employees
                        .Where(emp => emp.DepartmentId == query.DepartmentId.Value);
        }

        if (query.MaxSalary.HasValue)
        {
            employees = employees
                        .Where(emp => emp.Salary <= query.MaxSalary.Value);
        }

        if (query.MinSalary.HasValue)
        {
            employees = employees
                        .Where(emp => emp.Salary >= query.MinSalary.Value);
        }

        return await employees
                        .OrderBy(emp => emp.Id)
                        .Skip((query.Page - 1) * query.PageSize)
                        .Take(query.PageSize)
                        .Select(emp => new EmplyeeResponse
                        {
                            Name = emp.Name,
                            Id = emp.Id,
                            Email = emp.Email,
                            Salary = emp.Salary,
                            DepartmentName = emp.Department.Name,
                            DepartmentId = emp.DepartmentId
                        })
                        .ToListAsync();
        // return await _context.Employees
        //             .Include(emp => emp.Department)
        //             .AsNoTracking()
        //             .Select(emp => new EmplyeeResponse
        //             {
        //                 Id = emp.Id,
        //                 Name = emp.Name,
        //                 Email = emp.Email,
        //                 DepartmentId = emp.DepartmentId,
        //                 DepartmentName = emp.Department.Name,
        //                 Salary = emp.Salary
        //             })
        //             .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeById(Guid id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<Employee?> GetEmployeeByEmail(string email)
    {
        return await _context.Employees.FirstOrDefaultAsync(emp => emp.Email == email);
    }

    public async Task<bool> CreateEmployee(CreateEmployee employee)
    {
        Employee newEmployee = new Employee
        {
            Name = employee.Name,
            Email = employee.Email,
            DepartmentId = employee.DepartmentId,
            Salary = employee.Salary
        };
        _context.Employees.Add(newEmployee);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateEmployee(Employee employee)
    {
        _context.Employees.Update(employee);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteEmployee(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
}
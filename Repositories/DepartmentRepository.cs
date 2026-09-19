using EmployeeManagement.Entity;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly EmployeeDbContext _context;

    public DepartmentRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentResponse>> GetDepartments()
    {
        // return await _context.Departments
        //                 // .Include(emp => emp.Employees)
        //                 .AsNoTracking()
        //                 .Select(dep => new DepartmentResponse
        //                 {
        //                     Id = dep.Id,
        //                     Name = dep.Name,
        //                     Employees = dep.Employees.Select(emp => new DepartmentEmployeeResponse
        //                     {
        //                         Name = emp.Name,
        //                         Id = emp.Id
        //                     }).ToList()
        //                 })
        //                 .ToListAsync();
        return await _context.Departments
        .Include(d => d.Employees)
        .AsNoTracking()
        .Select(dep => new DepartmentResponse
        {
            Id = dep.Id,
            Name = dep.Name,
            Employees = dep.Employees.Select(emp => new DepartmentEmployeeResponse
            {
                Id = emp.Id,
                Name = emp.Name
            }).ToList()
        })
        .ToListAsync();
    }

    public async Task<Department?> GetDepartmentById(Guid id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<bool> CreateDepartment(CreateDepartment department)
    {
        Department newDepartment = new Department
        {
            Name = department.Name
        };
        _context.Departments.Add(newDepartment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateDepartment(Department department)
    {
        _context.Departments.Update(department);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteDepartment(Guid id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department != null)
        {
            _context.Departments.Remove(department);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
}
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Entity;

namespace EmployeeManagement.Data;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(
        DbContextOptions<EmployeeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
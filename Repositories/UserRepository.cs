using EmployeeManagement.Data;
using EmployeeManagement.Entity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EmployeeDbContext _context;

    public UserRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
}
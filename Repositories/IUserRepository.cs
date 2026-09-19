using EmployeeManagement.Entity;

namespace EmployeeManagement.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
}

// Seperate Entity and Model logic
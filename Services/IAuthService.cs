using EmployeeManagement.Models;

namespace EmployeeManagement.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> SignupAsync(SignupRequest request);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagement.Entity;
using EmployeeManagement.Enums;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponse> SignupAsync(SignupRequest request)
    {
        // 1. Check whether user already exists
        var existingUser = await _userRepository
            .GetByEmailAsync(request.Email);

        if (existingUser != null)
        {
            throw new Exception("User with this email already exists.");
        }

        // 2. Hash the password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(
            request.Password
        );

        // 3. Create database entity
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = Role.User
        };

        // 4. Save user
        var createdUser = await _userRepository
            .CreateUserAsync(user);

        // 5. Generate JWT
        var token = GenerateToken(createdUser);

        // 6. Return DTO
        return new LoginResponse
        {
            Token = token,
            UserId = createdUser.Id,
            Email = createdUser.Email,
            Role = createdUser.Role.ToString()
        };
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        // 1. Find user
        var user = await _userRepository
            .GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception("Invalid email or password.");
        }

        // 2. Verify password
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash
        );

        if (!isPasswordValid)
        {
            throw new Exception("Invalid email or password.");
        }

        // 3. Generate JWT
        var token = GenerateToken(user);

        // 4. Return DTO
        return new LoginResponse
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()
            ),

            new Claim(
                ClaimTypes.Email,
                user.Email
            ),

            new Claim(
                ClaimTypes.Role,
                user.Role.ToString()
            )
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}
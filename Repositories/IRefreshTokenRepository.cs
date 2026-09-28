using EmployeeManagement.Entity;

namespace EmployeeManagement.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);

    Task<RefreshToken> CreateAsync(RefreshToken refreshToken);

    Task RevokeAsync(RefreshToken refreshToken);
}
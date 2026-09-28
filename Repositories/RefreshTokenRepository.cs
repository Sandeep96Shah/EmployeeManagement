using EmployeeManagement.Data;
using EmployeeManagement.Entity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly EmployeeDbContext _context;

    public RefreshTokenRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenHashAsync(
        string tokenHash)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
    }

    public async Task<RefreshToken> CreateAsync(
        RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);

        await _context.SaveChangesAsync();

        return refreshToken;
    }

    public async Task RevokeAsync(
        RefreshToken refreshToken)
    {
        refreshToken.IsRevoked = true;

        await _context.SaveChangesAsync();
    }
}
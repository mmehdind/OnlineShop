using OnlineShop.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Services.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterDto dto);

    Task<bool> LoginAsync(LoginDto dto);

    Task LogoutAsync();
}
using Microsoft.AspNetCore.Identity;
using OnlineShop.DTOs.Auth;
using OnlineShop.Models.Identity;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.FullName
        };

        var result = await _userManager
            .CreateAsync(user, dto.Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, "Customer");
 
        await _signInManager.SignInAsync(user, isPersistent: false);
        
        return result;

    }

    public async Task<bool> LoginAsync(LoginDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(
            dto.Email,
            dto.Password,
            isPersistent: true,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return false;

        return true;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
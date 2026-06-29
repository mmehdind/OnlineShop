using Microsoft.AspNetCore.Identity;
using OnlineShop.DTOs.Profile;
using OnlineShop.Models.Identity;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class ProfileService : IProfileService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICurrentUserService _currentUser;

    public ProfileService(
        UserManager<AppUser> userManager,
        ICurrentUserService currentUser)
    {
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public async Task<ProfileDto?> GetProfileAsync()
    {
        var user = await _userManager.FindByIdAsync(
            _currentUser.UserId);

        if (user == null)
            return null;

        return new ProfileDto
        {
            FullName = user.FullName,
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber ?? ""
        };
    }

    public async Task UpdateProfileAsync(
        UpdateProfileDto dto)
    {
        var user = await _userManager.FindByIdAsync(
            _currentUser.UserId);

        if (user == null)
            return;

        user.FullName = dto.FullName;
        user.PhoneNumber = dto.PhoneNumber;

        await _userManager.UpdateAsync(user);
    }

    public async Task ChangePasswordAsync(
        ChangePasswordDto dto)
    {
        if (dto.NewPassword != dto.ConfirmPassword)
            throw new Exception("Passwords do not match");

        var user = await _userManager.FindByIdAsync(
            _currentUser.UserId);

        if (user == null)
            return;

        var result =
            await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword);

        if (!result.Succeeded)
        {
            throw new Exception(
                string.Join(
                    ",",
                    result.Errors.Select(x => x.Description)));
        }
    }
}
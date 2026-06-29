using OnlineShop.DTOs.Profile;

namespace OnlineShop.Services.Interfaces;

public interface IProfileService
{
    Task<ProfileDto?> GetProfileAsync();

    Task UpdateProfileAsync(
        UpdateProfileDto dto);

    Task ChangePasswordAsync(
        ChangePasswordDto dto);
}
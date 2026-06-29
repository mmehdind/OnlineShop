using System.ComponentModel.DataAnnotations;
using OnlineShop.Common;

namespace OnlineShop.ViewModels.Auth;

public class RegisterVm
{

    [Required]
    [StringLength(100, MinimumLength = 3)]
    [Display(Name = SiteTexts.Navigation.FullName)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    [Display(Name = SiteTexts.Navigation.Email)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    [Display(Name = SiteTexts.Navigation.PassWorld)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password),
        ErrorMessage = "Passwords do not match.")]
    [Display(Name = SiteTexts.Navigation.ConfirmPassword)]
    public string ConfirmPassword { get; set; } = string.Empty;
}
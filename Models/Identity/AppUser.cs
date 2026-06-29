using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Models.Identity;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
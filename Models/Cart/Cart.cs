using OnlineShop.Models.Base;
using OnlineShop.Models.Identity;


namespace OnlineShop.Models;

public class Cart : BaseEntity
{
    public string AppUserId { get; set; }

    public AppUser AppUser { get; set; } = null!;

    public ICollection<CartItem> Items { get; set; } = [];
}
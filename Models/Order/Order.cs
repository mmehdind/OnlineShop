using OnlineShop.Models.Base;
using OnlineShop.Models.Identity;

namespace OnlineShop.Models;

public class Order : BaseEntity
{
    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;

    public int AddressId { get; set; }
    public Address Address { get; set; } = null!;

    public long TotalPrice { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
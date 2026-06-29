using OnlineShop.Models.Base;

namespace OnlineShop.Models;

public class OrderStatusHistory : BaseEntity
{
    public int OrderId { get; set; }

    public Order Order { get; set; } = null!;

    public OrderStatus Status { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public string? Note { get; set; }
}
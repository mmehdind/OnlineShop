using OnlineShop.Models;

namespace OnlineShop.DTOs.Admin;

public class RecentOrderDto
{
public int Id { get; set; }

public string CustomerName { get; set; } = string.Empty;

public long TotalPrice { get; set; }

public OrderStatus Status { get; set; }

public DateTime CreatedAt { get; set; }

}

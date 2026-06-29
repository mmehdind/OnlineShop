using OnlineShop.Models;

namespace OnlineShop.ViewModels.Admin;

public class RecentOrderVm
{
public int Id { get; set; }

public string CustomerName { get; set; } = string.Empty;

public long TotalPrice { get; set; }

public OrderStatus Status { get; set; }

public DateTime CreatedAt { get; set; }

}

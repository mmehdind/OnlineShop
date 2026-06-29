namespace OnlineShop.Admin.ViewModels.Order;

using OnlineShop.Models;

public class OrderDetailsVm
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public long TotalPrice { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderItemDetailsVm> Items { get; set; } = [];
}
using OnlineShop.Models;

namespace OnlineShop.Admin.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public long TotalPrice { get; set; }

    public OrderStatus Status { get; set; }

    public List<OrderItemDto> Items { get; set; } = [];
}
using OnlineShop.Models;

namespace OnlineShop.Admin.DTOs.Order;

public class UpdateOrderStatusDto
{
    public int OrderId { get; set; }

    public OrderStatus Status { get; set; }
}
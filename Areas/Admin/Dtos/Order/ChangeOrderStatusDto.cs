using OnlineShop.Models;

namespace OnlineShop.Admin.DTOs.Order;

public class ChangeOrderStatusDto
{
    public int OrderId { get; set; }

    public OrderStatus Status { get; set; }
}
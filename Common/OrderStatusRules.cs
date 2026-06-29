using OnlineShop.Models;

namespace OnlineShop.Common;

public static class OrderStatusRules
{
    private static readonly Dictionary<OrderStatus, List<OrderStatus>> Allowed = new()
    {
        { OrderStatus.Pending, new() { OrderStatus.Paid, OrderStatus.Cancelled } },
        { OrderStatus.Paid, new() { OrderStatus.Processing, OrderStatus.Cancelled } },
        { OrderStatus.Processing, new() { OrderStatus.Shipped, OrderStatus.Cancelled } },
        { OrderStatus.Shipped, new() { OrderStatus.Cancelled } }
    };

    public static bool CanChange(OrderStatus current, OrderStatus next)
    {
        return Allowed.ContainsKey(current) &&
               Allowed[current].Contains(next);
    }
}
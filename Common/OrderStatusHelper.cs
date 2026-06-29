using OnlineShop.Models;

namespace OnlineShop.Common;

public static class OrderStatusHelper
{
public static string GetBadgeClass(OrderStatus status)
{
return status switch
{
OrderStatus.Pending => "warning",
OrderStatus.Paid => "success",
OrderStatus.Processing => "primary",
OrderStatus.Shipped => "info",
OrderStatus.Cancelled => "danger",
_ => "secondary"
};
}
}

using OnlineShop.Models;

namespace OnlineShop.Admin.ViewModels.Order;

public class OrderListAdminVm
{
    public List<OrderItemVm> Items { get; set; } = [];

    public OrderStatus? StatusFilter { get; set; }

    public int TotalCount { get; set; }

    public int PendingCount { get; set; }

    public int PaidCount { get; set; }

    public int ProcessingCount { get; set; }

    public int ShippedCount { get; set; }

    public int CancelledCount { get; set; }
}
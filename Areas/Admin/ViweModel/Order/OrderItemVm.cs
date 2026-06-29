namespace OnlineShop.Admin.ViewModels.Order;

public class OrderItemVm
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public long TotalPrice { get; set; }

    public string Status { get; set; } = string.Empty;
}
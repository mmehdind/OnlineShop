namespace OnlineShop.Admin.ViewModels.Order;

public class OrderItemDetailsVm
{
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public long Price { get; set; }
}
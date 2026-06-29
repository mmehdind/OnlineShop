namespace OnlineShop.Admin.DTOs.Order;

public class OrderItemDetailDto
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public long Price { get; set; }

    public long Total => Quantity * Price;
}
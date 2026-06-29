namespace OnlineShop.Admin.DTOs.Order;

public class OrderDetailsDto
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public long TotalPrice { get; set; }

    public string Address { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public List<OrderItemDetailDto> Items { get; set; } = [];
}
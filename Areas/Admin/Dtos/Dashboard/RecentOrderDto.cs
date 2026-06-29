namespace OnlineShop.Admin.DTOs.Dashboard;

public class RecentOrderDto
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public long TotalPrice { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
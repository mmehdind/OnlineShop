namespace OnlineShop.Admin.DTOs.Dashboard;

public class LowStockProductDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Stock { get; set; }
}
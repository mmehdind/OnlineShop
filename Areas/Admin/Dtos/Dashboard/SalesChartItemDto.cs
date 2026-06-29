namespace OnlineShop.Admin.DTOs.Dashboard;

public class SalesChartItemDto
{
    public string Label { get; set; } = string.Empty;

    public int OrderCount { get; set; }

    public long Revenue { get; set; }
}
namespace OnlineShop.Admin.DTOs.Dashboard;

public class SalesChartDto
{
    public ChartPeriod Period { get; set; }

    public List<SalesChartItemDto> Items { get; set; } = [];
}
namespace OnlineShop.Admin.DTOs.Dashboard;

public class DashboardDto
{
    public DashboardStatsDto Stats { get; set; } = new();

    public List<RecentOrderDto> RecentOrders { get; set; } = [];

    public List<LowStockProductDto> LowStockProducts { get; set; } = [];

    public SalesChartDto SalesChart { get; set; } = new();

    public ChartPeriod SelectedPeriod { get; set; } = new();
}
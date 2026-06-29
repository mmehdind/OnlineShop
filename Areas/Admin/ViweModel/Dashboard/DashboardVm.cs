using OnlineShop.Admin.DTOs.Dashboard;

namespace OnlineShop.Admin.ViewModels.Dashboard;

public class DashboardVm
{
    public DashboardStatsVm Stats { get; set; } = new();

    public List<RecentOrderVm> RecentOrders { get; set; } = [];

    public List<LowStockProductVm> LowStockProducts { get; set; } = [];

    public SalesChartVm SalesChart { get; set; } = new();

    public ChartPeriod SelectedPeriod { get; set; } = new();
}